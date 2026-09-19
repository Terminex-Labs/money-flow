using StackExchange.Redis;
using Shared.Cache.Abstraction;
using Microsoft.AspNetCore.DataProtection;
using Terminex.Security.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Medallion.Threading.Redis;
using Medallion.Threading;
using Shared.Authentication.Contracts.Requests;
using Terminex.Security.Configuration.Crypto;
using MoneyFlow.Bff.Services;
using Shared.Client.Abstraction;

namespace MoneyFlow.Bff.Extensions
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection UseCookie(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Terminex";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    options.Cookie.HttpOnly = true;
                    options.SlidingExpiration = true; // Динамическое продление времени жизни

                    if (environment.IsDevelopment())
                    {
                        options.Cookie.Domain = "127.0.0.1";
                        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                        options.Cookie.SameSite = SameSiteMode.Lax;
                    }
                    else
                    {
                        options.Cookie.Domain = ".terminex.ru";
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.Cookie.SameSite = SameSiteMode.Strict;
                    }

                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };

                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    };

                    options.Events.OnValidatePrincipal = async context =>
                    {
                        var sessionId = context.Principal?.FindFirst("SessionId")?.Value;

                        if (string.IsNullOrWhiteSpace(sessionId))
                        {
                            // Используется для отмены/аннулирования текущей аутентификации пользователя при проверке подлинности на основе cookie
                            context.RejectPrincipal();
                            return;
                        }

                        var sessionKey = CacheKeys.SessionString(sessionId);
                        var redisService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
                        var userSessionResult = await redisService.GetJsonAsync<UserSession>(sessionKey);

                        var cryptoService = context.HttpContext.RequestServices.GetRequiredService<ICryptoService>();
                        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                        var sessionDek = Convert.FromBase64String(configuration.GetValue<string>("Redis:EncryptedKeySession") 
                            ?? throw new InvalidOperationException("Шифрованный ключ сессии не настроен!"));
                        
                        if (userSessionResult.IsFailure)
                        {
                            // Используется для отмены/аннулирования текущей аутентификации пользователя при проверке подлинности на основе cookie
                            context.RejectPrincipal();
                            return;
                        }

                        var userSession = userSessionResult.Value;

                        var oneMinute = DateTime.UtcNow.AddMinutes(1);
                        
                        if (userSession.AccessTokenExpiresAt <= oneMinute)
                        {
                            var lockProvider = context.HttpContext.RequestServices.GetRequiredService<IDistributedLockProvider>();

                            var lockKey = CacheKeys.LockKeyString(sessionId);

                            await using var handle = await lockProvider.TryAcquireLockAsync(lockKey, TimeSpan.FromSeconds(5));

                            if (handle == null)
                            {
                                // Используется для отмены/аннулирования текущей аутентификации пользователя при проверке подлинности на основе cookie
                                context.RejectPrincipal();
                                return;
                            }

                            var reGetUserSessionResult = await redisService.GetJsonAsync<UserSession>(sessionKey);

                            if (reGetUserSessionResult.IsFailure)
                            {
                                // Используется для отмены/аннулирования текущей аутентификации пользователя при проверке подлинности на основе cookie
                                context.RejectPrincipal();
                                return;
                            }

                            userSession = reGetUserSessionResult.Value;

                            if (userSession.AccessTokenExpiresAt <= oneMinute)
                            {
                                var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthClient>();

                                var authResponseResult = await authService.RefreshToken(new RefreshTokenRequest(cryptoService.Decrypt<string>(userSession.EncryptedRefreshToken, sessionDek)!));

                                if (authResponseResult.IsSuccess)
                                {
                                    var authResponse = authResponseResult.Value;

                                    var jwtReader = context.HttpContext.RequestServices.GetRequiredService<IJwtReader>();

                                    var extract = jwtReader.Extract(authResponse.AccessToken);

                                    userSession.EncryptedAccessToken = cryptoService.Encrypt(authResponse.AccessToken, sessionDek, CryptoVersion.V1);
                                    userSession.EncryptedRefreshToken = cryptoService.Encrypt(authResponse.RefreshToken, sessionDek, CryptoVersion.V1);
                                    userSession.AccessTokenExpiresAt = extract.ExpiredTime;
                                    
                                    await redisService.SetJsonAsync(sessionKey, userSession, TimeSpan.FromDays(30));
                                }
                                else
                                {
                                    await redisService.RemoveAsync(sessionKey);
                                    context.RejectPrincipal();
                                    return;
                                }
                            }
                        }

                        context.HttpContext.Items["AccessToken"] = cryptoService.Decrypt<string>(userSession.EncryptedAccessToken, sessionDek);
                    };
                });

            return services;
        }

        public static IServiceCollection UseDistributedLock(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedLockProvider>(serviceProvider =>
            {
                var redis = serviceProvider.GetRequiredService<IConnectionMultiplexer>();

                return new RedisDistributedSynchronizationProvider(redis.GetDatabase());
            });

            return services;
        }

        public static IServiceCollection UseSharedCryptoKeyASPNET(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["Redis:ConnectionString"]!;
            var redis = ConnectionMultiplexer.Connect(connectionString);

            services.AddDataProtection().SetApplicationName("Terminex.SharedBff").PersistKeysToStackExchangeRedis(redis, CacheKeys.DataProtectionString());

            return services;
        }

        public static IServiceCollection UseCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy
                    (
                        name: "AllowSpecificOrigin",
                            policy =>
                            {
                                policy.WithOrigins("http://127.0.0.1:4300")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                            }
                    );
            });

            return services;
        }
    }
}
using MediatR;
using System.Reflection;
using Shared.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyFlow.Catalog.Application.Extensions
{
    public static class MediatRServiceCollectionExtensions
    {
        public static IServiceCollection UseMediatR(this IServiceCollection services)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));

            return services;
        }
    }
}
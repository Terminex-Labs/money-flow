using System.Net.Http.Headers;

namespace MoneyFlow.Bff.Handlers
{
    public sealed class AccessTokenHandler(IHttpContextAccessor accessor) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = accessor.HttpContext?.Items["AccessToken"] as string;

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
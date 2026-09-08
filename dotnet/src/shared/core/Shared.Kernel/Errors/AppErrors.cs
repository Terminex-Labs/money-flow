using Terminex.Common.Results;

namespace Shared.Kernel.Errors
{
    public static class AppErrors
    {
        public static readonly ErrorCode BackendHttp = ErrorCode.Custom(nameof(BackendHttp), 10001);
        public static readonly ErrorCode RequestCancelled = ErrorCode.Custom(nameof(RequestCancelled), 10002);
    }
}
using Terminex.Common.Results;

namespace Shared.Kernel.Errors
{
    public static class AppErrors
    {
        public static readonly ErrorCode BackendHttp = ErrorCode.Custom(nameof(BackendHttp), 10001);
        public static readonly ErrorCode RequestCancelled = ErrorCode.Custom(nameof(RequestCancelled), 10002);
        public static readonly ErrorCode IncorrectName = ErrorCode.Custom(nameof(IncorrectName), 10003);
        public static readonly ErrorCode Duplicate = ErrorCode.Custom(nameof(Duplicate), 10004);
        public static readonly ErrorCode SessionExpired = ErrorCode.Custom(nameof(SessionExpired), 10005);
        public static readonly ErrorCode IncorrectValue = ErrorCode.Custom(nameof(IncorrectValue), 10006);
        public static readonly ErrorCode IncorrectOperation = ErrorCode.Custom(nameof(IncorrectOperation), 10007);
    }
}
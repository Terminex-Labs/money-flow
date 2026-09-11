namespace Shared.Cache.Abstraction
{
    public static class CacheKeys
    {
        private const string TERMINEX = "terminex";

        public static string SessionString(string sessionId)
            => $"{TERMINEX}:sessions:{sessionId}";

        public static string DataProtectionString()
            => $"{TERMINEX}:shared:bff:data-protection-keys";

        public static string LockKeyString(string value)
            => $"{TERMINEX}:shared:lock:sessions:{value}";
    }
}
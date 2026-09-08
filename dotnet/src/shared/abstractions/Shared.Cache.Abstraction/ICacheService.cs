namespace Shared.Cache.Abstraction
{
    public interface ICacheService
    {
        Task<bool> RemoveAsync(string key);
        Task<bool> SetJsonAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T?> GetJsonAsync<T>(string key);
        Task<bool> SetRemoveAsync(string key, string value);
    }
}
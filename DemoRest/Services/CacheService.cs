namespace DemoRest.Services;

public class CacheService : ICacheService
{
    private readonly Dictionary<string, (object Value, DateTime? Expiration)> _cache;

    public CacheService()
    {
        _cache = new Dictionary<string, (object Value, DateTime? Expiration)>();
    }

    public void Set(string key, object value, TimeSpan? expiration = null)
    {
        var expirationTime = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : (DateTime?)null;
        _cache[key] = (value, expirationTime);
    }

    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var cacheEntry))
        {
            if (cacheEntry.Expiration == null || cacheEntry.Expiration > DateTime.UtcNow)
            {
                return (T)cacheEntry.Value;
            }
            else
            {
                _cache.Remove(key); // Remove expired entry
            }
        }
        return default;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}

public interface ICacheService
{
    void Set(string key, object value, TimeSpan? expiration = null);
    T? Get<T>(string key);
    void Remove(string key);
}

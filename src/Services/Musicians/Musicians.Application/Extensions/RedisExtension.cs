using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Musicians.Application.Extensions
{
    public static class RedisExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string key,
            T value,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken = default)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, jsonData, options, cancellationToken);
        }

        public static bool TryGetRecord<T>(this IDistributedCache cache,
            string recordId,
            out T? value)
        {
            var jsonData = cache.Get(recordId);
            value = default(T);

            if (jsonData == null)
            {
                return false;
            }

            value = JsonSerializer.Deserialize<T>(jsonData);
            return true;
        }

        public static DistributedCacheEntryOptions CreateDistributedCacheEntryOptions(this IDistributedCache cache,
            TimeSpan? absoluteExpirationMinutes,
            TimeSpan? slidingExpirationMinutes) =>
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationMinutes ?? TimeSpan.FromMinutes(5),
                SlidingExpiration = slidingExpirationMinutes,
            };
    }
}

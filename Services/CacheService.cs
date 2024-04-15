using Microsoft.Extensions.Caching.Memory;
using MyJobDashboard.Services.Interfaces;

namespace MyJobDashboard.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly string _tokenKey;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _tokenKey = "access_token";
        }

        /// <summary>
        /// Sets the access token
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        public void SetToken(string value, TimeSpan expirationTime)
        {
            var check = _cache.Get(_tokenKey); //check if already exists
            if (check != null)
                return;

            _cache.Set(_tokenKey, value, expirationTime);
        }

        /// <summary>
        /// Gets the access token
        /// </summary>
        /// <returns></returns>
        public string? GetToken()
        {
            return (string?)(_cache.Get(_tokenKey) ?? null);
        }
    }
}


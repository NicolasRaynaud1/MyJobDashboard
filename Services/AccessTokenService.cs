using DashboardJob.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using MyJobDashboard.Models;

namespace DashboardJob.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly string _tokenKey;
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }
        public string FranceTravailUri { get; set; }
        public string Scope { get; set; }

        public AccessTokenService(IConfiguration config, IMemoryCache cache)
        {
            _configuration = config;
            _cache = cache;
            _tokenKey = "access_token";
            ClientId = _configuration["Env:Credentials:ClientId"] ?? "";
            ClientSecret = _configuration["Env:Credentials:ClientSecret"] ?? "";
            FranceTravailUri = _configuration["Env:FranceTravailUri"] ?? "";
            Scope = _configuration["Env:Scope"] ?? "";
        }

        /// <summary>
        /// Call to FranceTravail to get an access token to their Apis
        /// </summary>
        /// <returns></returns>
        public async Task<AccessToken?> GenerateAccessTokenAsync()
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", ClientId.ToString()),
                new KeyValuePair<string, string>("client_secret", ClientSecret.ToString()),
                new KeyValuePair<string, string>("scope", Scope.ToString()),
            });

            try
            {
                var response = await client.PostAsync(FranceTravailUri, content);

                return await response.Content.ReadFromJsonAsync<AccessToken>() ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAccessTokenAsync Failed", ex);
                return null;
            }
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
            var token = (string?)(_cache.Get(_tokenKey));

            if (token is null)
                token = GenerateAccessTokenAsync()?.Result?.TokenString;

            return token;
        }
    }
}

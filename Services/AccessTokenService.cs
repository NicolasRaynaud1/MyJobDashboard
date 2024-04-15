using DashboardJob.Services.Interfaces;
using MyJobDashboard.Models;

namespace DashboardJob.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IConfiguration _configuration;
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }
        public string FranceTravailUri { get; set; }
        public string Scope { get; set; }

        public AccessTokenService(IConfiguration config)
        {
            _configuration = config;
            ClientId = _configuration["Env:Credentials:ClientId"] ?? "";
            ClientSecret = _configuration["Env:Credentials:ClientSecret"] ?? "";
            FranceTravailUri = _configuration["Env:FranceTravailUri"] ?? "";
            Scope = _configuration["Env:Scope"] ?? "";
        }

        /// <summary>
        /// Call to FranceTravail to get an access token to their Apis
        /// </summary>
        /// <returns></returns>
        public async Task<AccessToken?> GetAccessTokenAsync()
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
    }
}

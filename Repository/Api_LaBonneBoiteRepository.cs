using DashboardJob.Services.Interfaces;
using MyJobDashboard.Repository.Interfaces;
using System.Net.Http.Headers;

namespace MyJobDashboard.Repository
{
    public class Api_LaBonneBoiteRepository : IApi_LaBonneBoiteRepository
    {
        private readonly IAccessTokenService _accessTokenService;
        public Api_LaBonneBoiteRepository(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public async Task<string> getCompaniesAsync()
        {
            var token = _accessTokenService.GetToken(); // get the access token in cache (or generate one if needed)

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("https://api.francetravail.io/partenaire/labonneboite/v1/company/?departments=31&rome_codes=M1805");
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
    }
}

using MyJobDashboard.Models;

namespace DashboardJob.Services.Interfaces
{
    public interface IAccessTokenService
    {
        Task<AccessToken?> GenerateAccessTokenAsync();

        string? GetToken();

        void SetToken(string value, TimeSpan expirationTime);
    }
}

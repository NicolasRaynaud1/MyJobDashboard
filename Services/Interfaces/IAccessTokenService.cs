using MyJobDashboard.Models;

namespace DashboardJob.Services.Interfaces
{
    public interface IAccessTokenService
    {
        Task<AccessToken?> GetAccessTokenAsync();
    }
}

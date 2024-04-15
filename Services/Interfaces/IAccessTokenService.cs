namespace DashboardJob.Services.Interfaces
{
    public interface IAccessTokenService
    {
        Task<string> GetAccessTokenAsync();
    }
}

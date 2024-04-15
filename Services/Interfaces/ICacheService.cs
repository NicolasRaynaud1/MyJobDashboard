namespace MyJobDashboard.Services.Interfaces
{
    public interface ICacheService
    {
        void SetToken(string value, TimeSpan expirationTime);

        string? GetToken();
    }
}

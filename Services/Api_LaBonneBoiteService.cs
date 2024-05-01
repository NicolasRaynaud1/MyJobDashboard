using MyJobDashboard.Repository.Interfaces;
using MyJobDashboard.Services.Interfaces;

namespace MyJobDashboard.Services
{
    public class Api_LaBonneBoiteService : IApi_LaBonneBoiteService
    {
        private readonly IApi_LaBonneBoiteRepository _repository;

        public Api_LaBonneBoiteService(IApi_LaBonneBoiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetCompaniesAsync()
        {
            return await _repository.getCompaniesAsync();
        }
    }
}

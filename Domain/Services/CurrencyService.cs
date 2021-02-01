using Domain.Model;
using Refit;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyApi _api;

        public CurrencyService(ICurrencyApi api)
        {
            _api = api;
        }

        public async Task<ApiResponse<Rates>> GetLatest()
        {
            return await _api.GetLatest();
        }
    }
}

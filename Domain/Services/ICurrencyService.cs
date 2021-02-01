using Domain.Model;
using Refit;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICurrencyService
    {
        Task<ApiResponse<Rates>> GetLatest();
    }
}

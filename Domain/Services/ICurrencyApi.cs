using Domain.Model;
using Refit;
using System.Threading.Tasks;

namespace Domain.Services
{
    [Headers("Content-Type: application/json")]
    public interface ICurrencyApi
    {
        [Get("/latest")]
        Task<ApiResponse<Rates>> GetLatest();

        [Get("/{date}")]
        Task<ApiResponse<Rates>> GetDate(string date, string symbols, string @base = "EUR");
    }
}

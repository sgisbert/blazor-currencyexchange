using Domain.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICurrencyService
    {
        Task<Rates> GetLatest();
        Dictionary<string, string> GetCurrencies();
        Task<Rates> GetDate(DateTime date, string symbols = "", string @base = "EUR");
        Task<DateTime> GetLatestDate();
    }
}

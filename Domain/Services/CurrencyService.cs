using Domain.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyApi _api;
        private Rates _latestRates;
        private Rates _rates;
        private Dictionary<string, string> _currencies;

        public CurrencyService(ICurrencyApi api)
        {
            _api = api;
            var task = GetLatest();
            Task.WaitAll(task);
            _latestRates = task.Result;
            _rates = _latestRates;
        }

        public async Task<Rates> GetLatest()
        {
            if (_latestRates != null)
                return _latestRates;

            var result = await _api.GetLatest().ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _latestRates = result.Content;
                _rates = _latestRates;
                _currencies = SetCurrencies();
            }

            return _latestRates;
        }

        public async Task<Rates> GetDate(DateTime date)
        {
            var request = await _api.GetDate(date.ToString("yyyy-MM-dd"));
            if (request.IsSuccessStatusCode)
            {
                return request.Content;
            }
            return null;
        }

        public async Task<DateTime> GetLatestDate()
        {
            DateTime _latestDate;
            var result = await GetLatest();
            DateTime.TryParse(_latestRates.Date.ToString("d"), out _latestDate);
            return _latestDate;
        }

        public Dictionary<string, string> GetCurrencies()
        {
            if (_currencies != null)
                return _currencies;

            _currencies = SetCurrencies();
            return _currencies;
        }

        private Dictionary<string, string> SetCurrencies()
        {
            List<string> currencyCodes = new List<string>();

            if (_rates != null && _rates.RateList.Any())
                currencyCodes = _rates.RateList.Select(d => d.Key).OrderBy(c => c).ToList();
            else
                return null;

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(ci => ci.LCID).Distinct();

            List<RegionInfo> regions = new List<RegionInfo>();
            foreach (var culture in cultures)
            {
                try
                {
                    regions.Add(new RegionInfo(culture));
                }
                catch (System.Exception)
                {
                }
            }
            return regions
                .GroupBy(r => r.ISOCurrencySymbol)
                .Select(g => g.First())
                .Select(r => new
                {
                    r.ISOCurrencySymbol,
                    r.CurrencyEnglishName
                })
                .Where(c => currencyCodes.Contains(c.ISOCurrencySymbol))
                .ToDictionary(k => k.ISOCurrencySymbol, v => v.CurrencyEnglishName);
        }
    }
}

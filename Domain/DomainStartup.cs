using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Domain
{
    public static class DomainStartup
    {
        public static void InitDomain(this IServiceCollection services)
        {
            services.AddSingleton<ICurrencyService, CurrencyService>();

            services.AddRefitClient<ICurrencyApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.exchangeratesapi.io/"))
                    .SetHandlerLifetime(TimeSpan.FromMinutes(2));
        }
    }
}

using HttpClientDiagnostics;
using Refit;
using System;
using System.Net.Http;

namespace WeeklyXamarin.Api.Services
{
    public class CuratedApiService
    {
        private const string ApiBaseAddress = "https://api.curated.co";
        private readonly Lazy<ICuratedApi> _curatedApi;

        public CuratedApiService(HttpClientHandler authenticatedClientHanbdler, string apiBaseAddress = null, bool enableDiagnostics = false)
        {
            if (authenticatedClientHanbdler == null)
            {
                throw new ArgumentNullException(nameof(authenticatedClientHanbdler));
            }

            HttpMessageHandler handler;

            if (enableDiagnostics)
            {
                handler = new HttpClientDiagnosticsHandler(authenticatedClientHanbdler);
            }
            else
            {
                handler = authenticatedClientHanbdler;
            }

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(apiBaseAddress ?? ApiBaseAddress)
            };

            _curatedApi = new Lazy<ICuratedApi>(() => RestService.For<ICuratedApi>(client));
        }

        public ICuratedApi CuratedApi => _curatedApi.Value;
    }
}
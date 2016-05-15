using System;
using System.Net.Http;
using HttpClientDiagnostics;
using Refit;
using WeeklyXamarin.Services.Api;

namespace WeeklyXamarin.Services.Connected.Api
{
    public class WeeklyXamarinApiService : IWeeklyXamarinApiService
    {
        private const string ApiBaseAddress = "https://weeklyxamarin.azurewebsites.net";
        private readonly Lazy<IWeeklyXamarinApi> _weeklyXamarinApi;

        public WeeklyXamarinApiService(string apiBaseAddress = null, bool enableDiagnostics = false)
        {
            HttpMessageHandler handler;

            if (enableDiagnostics)
            {
                handler = new HttpClientDiagnosticsHandler(new HttpClientHandler());
            }
            else
            {
                handler = new HttpClientHandler();
            }

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(apiBaseAddress ?? ApiBaseAddress)
            };

            _weeklyXamarinApi = new Lazy<IWeeklyXamarinApi>(() => RestService.For<IWeeklyXamarinApi>(client));
        }

        public IWeeklyXamarinApi WeeklyXamarinApi => _weeklyXamarinApi.Value;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using WeeklyXamarin.Services.Api;
using WeeklyXamarin.Services.Connected.Api;
using WeeklyXamarin.Services.Connected.NetworkConnectivity;
using WeeklyXamarin.Services.Connected.State;
using WeeklyXamarin.Services.NetworkConnectivity;
using WeeklyXamarin.Services.State;
using Splat;
using WeeklyXamarin.Services.Connected.WeeklyXamarin;
using WeeklyXamarin.Services.WeeklyXamarin;

namespace WeeklyXamarin.App
{
    public abstract partial class CompositionRoot
    {
        protected readonly Lazy<IBlobCache> _blobCache;
        protected readonly Lazy<IWeeklyXamarinApiService> _duckDuckGoApiService;
        protected readonly Lazy<ILogger> _loggingService;
        protected readonly Lazy<INetworkConnectivityService> _networkConnectivityService;
        protected readonly Lazy<IWeeklyXamarinService> _weeklyXamarinService;
        protected readonly Lazy<IStateService> _stateService;
        public IWeeklyXamarinApiService ResolveWeeklyXamarinService() => _duckDuckGoApiService.Value;

        public ILogger ResolveLoggingService() => _loggingService.Value;
        public IWeeklyXamarinService ResolveSearchService() => _weeklyXamarinService.Value;

        public IStateService ResolveStateService() => _stateService.Value;
        protected abstract ILogger CreateLoggingService();

        private IBlobCache CreateBlobCache() => BlobCache.LocalMachine;

        private IWeeklyXamarinApiService CreateDuckDuckGoApiService() => new WeeklyXamarinApiService();

        private INetworkConnectivityService CreateNetworkConnectivityService() => new NetworkConnectivityService();

        private IWeeklyXamarinService CreateSearchService()
                    => new WeeklyXamarinService(ResolveWeeklyXamarinService(), ResolveStateService());

        private IStateService CreateStateService() => new StateService(ResolveBlobCache());
        private IBlobCache ResolveBlobCache() => _blobCache.Value;

        private INetworkConnectivityService ResolveNetworkConnectivityService() => _networkConnectivityService.Value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using WeeklyXamarin.Services.Api;
using WeeklyXamarin.Services.Connected.Api;
using WeeklyXamarin.Services.Connected.NetworkConnectivity;
using WeeklyXamarin.Services.Connected.Search;
using WeeklyXamarin.Services.Connected.State;
using WeeklyXamarin.Services.NetworkConnectivity;
using WeeklyXamarin.Services.Search;
using WeeklyXamarin.Services.State;
using Splat;

namespace WeeklyXamarin.App
{
    public abstract partial class CompositionRoot
    {
        protected readonly Lazy<IBlobCache> _blobCache;
        protected readonly Lazy<IDuckDuckGoApiService> _duckDuckGoApiService;
        protected readonly Lazy<ILogger> _loggingService;
        protected readonly Lazy<INetworkConnectivityService> _networkConnectivityService;
        protected readonly Lazy<ISearchService> _searchService;
        protected readonly Lazy<IStateService> _stateService;
        public IDuckDuckGoApiService ResolveDuckDuckGoApiService() => _duckDuckGoApiService.Value;

        public ILogger ResolveLoggingService() => _loggingService.Value;
        public ISearchService ResolveSearchService() => _searchService.Value;

        public IStateService ResolveStateService() => _stateService.Value;
        protected abstract ILogger CreateLoggingService();

        private IBlobCache CreateBlobCache() => BlobCache.LocalMachine;

        private IDuckDuckGoApiService CreateDuckDuckGoApiService() => new DuckDuckGoApiService();

        private INetworkConnectivityService CreateNetworkConnectivityService() => new NetworkConnectivityService();

        private ISearchService CreateSearchService()
                    => new SearchService(ResolveDuckDuckGoApiService(), ResolveStateService());

        private IStateService CreateStateService() => new StateService(ResolveBlobCache());
        private IBlobCache ResolveBlobCache() => _blobCache.Value;

        private INetworkConnectivityService ResolveNetworkConnectivityService() => _networkConnectivityService.Value;
    }
}

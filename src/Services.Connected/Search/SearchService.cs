using WeeklyXamarin.Core;
using WeeklyXamarin.Services.Api;
using WeeklyXamarin.Services.Search;
using WeeklyXamarin.Services.State;
using WeeklyXamarin.Utility;
using System;
using System.Reactive.Linq;

namespace WeeklyXamarin.Services.Connected.Search
{
    public class SearchService : ISearchService
    {
        private readonly IDuckDuckGoApiService _duckDuckGoApiService;
        private readonly IStateService _stateService;

        public SearchService(IDuckDuckGoApiService duckDuckGoApiService, IStateService stateService)
        {
            Ensure.ArgumentNotNull(duckDuckGoApiService, nameof(duckDuckGoApiService));
            Ensure.ArgumentNotNull(stateService, nameof(stateService));

            _duckDuckGoApiService = duckDuckGoApiService;
            _stateService = stateService;
        }

        public IObservable<DuckDuckGoSearchResult> Search(string query)
        {
            return _stateService.GetOrFetch(BlobCacheKeys.GetKeyForSearch(query),
                async () => await _duckDuckGoApiService.UserInitiated.Search(query), absoluteExpiration: DateTime.UtcNow.AddDays(7));
        }
    }
}
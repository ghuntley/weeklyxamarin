using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeklyXamarin.Services.Api;
using WeeklyXamarin.Services.NetworkConnectivity;
using WeeklyXamarin.Services.State;
using WeeklyXamarin.Utility;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Legacy;
using WeeklyXamarin.Services.WeeklyXamarin;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace WeeklyXamarin.ViewModels
{
    public class IssuesViewModel : ReactiveObject
    {
        private readonly INetworkConnectivityService _networkConnectivityService;
        private readonly IWeeklyXamarinService _weeklyXamarinService;

        public IssuesViewModel(INetworkConnectivityService networkConnectivityService, IWeeklyXamarinService weeklyXamarinService)
        {
            Ensure.ArgumentNotNull(networkConnectivityService, nameof(networkConnectivityService));
            Ensure.ArgumentNotNull(weeklyXamarinService, nameof(weeklyXamarinService));

            _networkConnectivityService = networkConnectivityService;
            _weeklyXamarinService = weeklyXamarinService;

            SearchResults = new ReactiveList<IssuesResult>();

            // Here we're describing here, in a *declarative way*, the conditions in
            // which the Search command is enabled.  Now our Command IsEnabled is
            // perfectly efficient, because we're only updating the UI in the scenario
            // when it should change.
            var canSearch = this.WhenAnyValue(vm => vm.SearchQuery, value => !string.IsNullOrWhiteSpace(value));

            // ReactiveCommand has built-in support for background operations and
            // guarantees that this block will only run exactly once at a time, and
            // that the CanExecute will auto-disable and that property IsExecuting will
            // be set according whilst it is running.
            Search = ReactiveCommand.CreateAsyncObservable(canSearch, x => _weeklyXamarinService.Search(SearchQuery));

            // ReactiveCommands are themselves IObservables, whose value are the results
            // from the async method, guaranteed to arrive on the UI thread. We're going
            // to take the list of search results that the background operation loaded,
            // and them into our SearchResults.
            Search.Subscribe(results =>
            {
                SearchResults.Clear();
                SearchResults.AddRange(results);
            });

            // ThrownExceptions is any exception thrown from the CreateAsyncTask piped
            // to this Observable. Subscribing to this allows you to handle errors on
            // the UI thread.
            Search.ThrownExceptions
                .Subscribe(ex => {
                    UserError.Throw("Potential Network Connectivity Error", ex);
                });

            // Whenever the Search query changes, we're going to wait for one second
            // of "dead airtime", then automatically invoke the subscribe command.
            this.WhenAnyValue(x => x.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.Search);
        }

        [Reactive]
        public string SearchQuery { get; set; }

        public ReactiveCommand<IEnumerable<IssuesResult>> Search { get; private set; }

        public ReactiveCommand<Unit> OpenWebBrowser { get; private set; }

        public ReactiveList<IssuesResult> SearchResults { get; private set; }
    }
}

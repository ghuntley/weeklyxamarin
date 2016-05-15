using System;
using ReactiveUI;
using WeeklyXamarin.Core;
using WeeklyXamarin.Services.Api;
using WeeklyXamarin.Services.State;
using WeeklyXamarin.Services.WeeklyXamarin;
using WeeklyXamarin.Utility;

namespace WeeklyXamarin.Services.Connected.WeeklyXamarin
{
    public class WeeklyXamarinService : IWeeklyXamarinService
    {
        private readonly IWeeklyXamarinApiService _weeklyXamarinApiService;
        private readonly IStateService _stateService;

        public WeeklyXamarinService(IWeeklyXamarinApiService weeklyXamarinApiService, IStateService stateService)
        {
            Ensure.ArgumentNotNull(weeklyXamarinApiService, nameof(weeklyXamarinApiService));
            Ensure.ArgumentNotNull(stateService, nameof(stateService));

            _weeklyXamarinApiService = weeklyXamarinApiService;
            _stateService = stateService; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuesPerPage"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IObservable<IssuesResult> Issues(int issuesPerPage = 250, int page = 1)
        {
            return _stateService.GetAndFetchLatest(BlobCacheKeys.GetKeyForIssues(issuesPerPage, page),
                _weeklyXamarinApiService.WeeklyXamarinApi.Issues(),
                fetchPredicate: dateTimeOffset => FetchOnlyIfCloseOrAfterUsualPublishDate(dateTimeOffset),
                absoluteExpiration: RxApp.MainThreadScheduler.Now.AddDays(7));
        }

        /// <summary>
        /// - this returns twice, once immediately with cached value if exists, once again with updated results if the date of the week is older than Wednesday.
        /// </summary>
        /// <param name="issueNumber"></param>
        /// <returns></returns>
        public IObservable<IssueResult> Issue(int issueNumber)
        {
            return _stateService.GetAndFetchLatest(BlobCacheKeys.GetKeyForIssue(issueNumber),
                _weeklyXamarinApiService.WeeklyXamarinApi.Issues(),
                fetchPredicate: dateTimeOffset => true,
                absoluteExpiration: RxApp.MainThreadScheduler.Now.AddDays(31));
        }

        private bool FetchOnlyIfCloseOrAfterUsualPublishDate(DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset.DayOfWeek >= DayOfWeek.Thursday)
            {
                return true;
            }

            return false;
        }
    }
}
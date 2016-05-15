using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace WeeklyXamarin.Services.Api
{
    public interface IWeeklyXamarinApi
    {
        [Get("/issues?per_page={issuesPerPage}&page={page}")]
        IObservable<IssuesResult> Issues(int issuesPerPage = 250, int page = 1);

        [Get("/issues?{issueNumber}")]
        IObservable<IssueResult> Issue(int issuesNumber);
    }
}

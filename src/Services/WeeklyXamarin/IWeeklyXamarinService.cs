using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using WeeklyXamarin.Services.Api;

namespace WeeklyXamarin.Services.WeeklyXamarin
{
    public interface IWeeklyXamarinService
    {
        IObservable<IssuesResult> Issues(int issuesPerPage = 250, int page = 1);
        IObservable<IssueResult> Issue(int issueNumber);
    }
}

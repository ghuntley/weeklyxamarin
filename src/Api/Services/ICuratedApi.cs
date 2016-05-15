using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Refit;

namespace WeeklyXamarin.Api.Services
{
    [Headers("Accept: application/json", "Content-type: application/json", "User-Agent: WeeklyXamarin Api")]

    public interface ICuratedApi
    {
        [Get("/api/v1/issues?per_page={issuesPerPage}&page={page}")]
        Task<string> Issues(int issuesPerPage = 10, int page = 1);

        [Get("/api/v1/issues/{issueNumber}")]
        Task<string> Issue(int issueNumber);
    }
}
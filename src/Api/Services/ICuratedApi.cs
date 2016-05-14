using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Refit;

namespace WeeklyXamarin.Api.Services
{
    [Headers("Accept: application/json", "User-Agent: WeeklyXamarin Api")]

    public interface ICuratedApi
    {
        [Get("/api/v1/issues?{issuesPerPage}&page={page}")]
        IObservable<string> Issues(int issuesPerPage = 10, int page = 1);
    }
}
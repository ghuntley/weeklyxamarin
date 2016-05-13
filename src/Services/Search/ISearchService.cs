using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using WeeklyXamarin.Services.Api;

namespace WeeklyXamarin.Services.Search
{
    public interface ISearchService
    {
        IObservable<IEnumerable<DuckDuckGoSearchResult>> Search(string query);
    }
}

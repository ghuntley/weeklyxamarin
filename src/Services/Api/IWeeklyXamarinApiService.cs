using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyXamarin.Services.Api
{
    public interface IWeeklyXamarinApiService
    {
        IWeeklyXamarinApi WeeklyXamarinApi { get; }
    }
}

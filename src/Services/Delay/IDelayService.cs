using System;
using System.Reactive;

namespace WeeklyXamarin.Services.Delay
{
    public interface IDelayService
    {
        IObservable<Unit> Delay(TimeSpan duration);
    }
}
using System.Reactive.Concurrency;
using WeeklyXamarin.Services.Connected.Delay;
using WeeklyXamarin.UnitTests.Services.Scheduler.Mocks;
using PCLMock;

namespace WeeklyXamarin.UnitTests.Services.Delay.Builders
{

    public sealed class DelayServiceBuilder : IBuilder
    {
        private IScheduler _scheduler;

        public DelayServiceBuilder()
        {
            _scheduler = new SchedulerMock(MockBehavior.Loose);
        }

        public DelayServiceBuilder WithScheduler(IScheduler scheduler) =>
            this.With(ref _scheduler, scheduler);

        public DelayService Build() =>
            new DelayService(_scheduler);

        public static implicit operator DelayService(DelayServiceBuilder builder) =>
            builder.Build();
    }
}
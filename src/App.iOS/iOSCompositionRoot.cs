using System;
using System.Collections.Generic;
using System.Text;
using WeeklyXamarin.App;
using WeeklyXamarin.Services.iOS.Logging;
using Splat;

namespace WeeklyXamarin.iOS
{
    public sealed class iOSCompositionRoot : CompositionRoot
    {
        protected override ILogger CreateLoggingService() => new LoggingService();
    }
}

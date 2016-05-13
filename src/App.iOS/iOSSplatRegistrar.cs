using WeeklyXamarin.App;

using Splat;

namespace WeeklyXamarin.iOS
{
    public sealed class iOSSplatRegistrar : SplatRegistrar
    {
        protected override void RegisterPlatformComponents(IMutableDependencyResolver splatLocator, CompositionRoot compositionRoot)
        {
            splatLocator.Register(compositionRoot.ResolveLoggingService, typeof(ILogger));
        }

        protected override void RegisterViews(IMutableDependencyResolver splatLocator)
        {
        }
    }
}
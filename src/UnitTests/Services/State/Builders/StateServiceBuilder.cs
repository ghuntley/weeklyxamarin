using Akavache;
using WeeklyXamarin.Services.Connected.State;
using WeeklyXamarin.UnitTests.Services.State.Mocks;
using PCLMock;

namespace WeeklyXamarin.UnitTests.Services.State.Builders
{

    internal sealed class StateServiceBuilder : IBuilder
    {
        private IBlobCache _blobCache;

        public StateServiceBuilder()
        {
            _blobCache = new BlobCacheMock(MockBehavior.Loose);
        }

        public StateServiceBuilder WithBlobCache(IBlobCache blobCache) =>
            this.With(ref _blobCache, blobCache);


        public StateService Build() =>
            new StateService(_blobCache);

        public static implicit operator StateService(StateServiceBuilder builder) =>
            builder.Build();
    }
}
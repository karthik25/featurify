using Featurify.Contracts;

namespace Featurify.State
{
    public class FeaturifyState<TFeature>
    {
        public FeaturifyState(IFeaturifyServer server)
        {
            Server = server;
        }

        public IFeaturifyServer Server { get; }
    }
}

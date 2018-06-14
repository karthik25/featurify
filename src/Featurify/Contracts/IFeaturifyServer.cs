using System.Threading.Tasks;

namespace Featurify.Contracts
{
    public interface IFeaturifyServer
    {
        Task<bool> Enabled<TFeature>()
            where TFeature : IFeatureToggle;
    }
}

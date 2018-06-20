using Featurify.Contracts;
using Featurify.State;
using System.Threading.Tasks;

namespace Featurify
{
    public static class FeaturifyFluentExtensions
    {
        public static FeaturifyState<TFeature> Is<TFeature>(this IFeaturifyServer toggler)
            where TFeature : IFeatureToggle
        {
            return new FeaturifyState<TFeature>(toggler);
        }

        public async static Task<bool> Enabled<TFeature>(this FeaturifyState<TFeature> state)
            where TFeature : IFeatureToggle
        {
            return await state.Server.Enabled<TFeature>();
        }        
    }
}

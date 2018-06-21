using System.Threading.Tasks;
using Featurify.Contracts;
using Featurify.Helpers;
using Microsoft.Extensions.Options;

namespace Featurify
{
    public class FeaturifyServer : IFeaturifyServer
    {
        private readonly IToggleMetadataFinder dataFinder;
        private readonly IUserInfoStrategy strategy;
        private readonly IFeatureNameTransformer transformer;
        private readonly FeaturifyOptions options;

        public FeaturifyServer(IToggleMetadataFinder dataFinder, 
                               IUserInfoStrategy strategy, 
                               IFeatureNameTransformer transformer,
                               IOptions<FeaturifyOptions> options)
        {
            this.dataFinder = dataFinder;
            this.strategy = strategy;
            this.transformer = transformer;
            this.options = options.Value;
        }

        public Task<bool> Enabled<TFeature>()
            where TFeature : IFeatureToggle
        {
            var featureName = typeof(TFeature).Name;
            return Enabled(featureName);
        }

        private async Task<bool> Enabled(string featureName)
        {
            var userId = await strategy.GetCurrentUserId();
            var transformedName = transformer.TransformFeatureName(featureName);
            var featureMetadata = await dataFinder.FindToggleStatus(transformedName, userId);
            Assertions.IsValid(featureMetadata, transformedName);
            return featureMetadata.IsFeatureEnabled(userId, options);
        }
    }
}

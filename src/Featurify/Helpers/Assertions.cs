using Featurify.Contracts;
using Featurify.Exceptions;

namespace Featurify.Helpers
{
    public static class Assertions
    {
        public static void IsValid(this IToggleMetadata metadata, string expectedFeatureName)
        {
            if (metadata.Name != expectedFeatureName)
            {
                throw new InvalidFeatureToggleMetadaException($"Expected {expectedFeatureName}, got {metadata.Name}");
            }
        }
    }
}

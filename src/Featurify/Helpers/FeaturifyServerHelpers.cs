using Featurify.Contracts;
using Featurify.Exceptions;

namespace Featurify.Helpers
{
    public static class FeaturifyServerHelpers
    {
        public static bool IsFeatureEnabled(this IToggleMetadata metadata, string userId, FeaturifyOptions options = null)
        {
            var validationOptions = BuildOptions(options);
            if (metadata.UserId == validationOptions.AnyUserVerifier)
                return metadata.Value;
            if (metadata.UserId == userId)
                return metadata.Value;
            if (validationOptions.UseStrict)
                throw new FeatureNotEnabledException($"This feature has not been enabled.");
            return false;
        }

        public static FeaturifyOptions BuildOptions(this FeaturifyOptions options)
        {
            return options ?? 
                   new FeaturifyOptions
                   {
                      AnyUserVerifier = FeaturifyConstants.DefaultAnyUserVerifier,
                      UseStrict = false
                   };
        }
    }    
}

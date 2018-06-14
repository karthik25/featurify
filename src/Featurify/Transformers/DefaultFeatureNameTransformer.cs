using Featurify.Contracts;
using System;

namespace Featurify.Transformers
{
    public class DefaultFeatureNameTransformer : IFeatureNameTransformer
    {
        public string TransformFeatureName(string featureName)
        {
            return $"Featurify.{featureName}";
        }
    }
}

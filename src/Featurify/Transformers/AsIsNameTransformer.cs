using Featurify.Contracts;
using System;

namespace Featurify.Transformers
{
    public class AsIsNameTransformer : IFeatureNameTransformer
    {
        public string TransformFeatureName(string featureName)
        {
            return featureName;
        }
    }
}

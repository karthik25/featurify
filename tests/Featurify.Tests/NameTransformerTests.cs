using Featurify.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Featurify.Tests
{
    [TestClass]
    public class NameTransformerTests
    {
        [TestMethod]
        public void CanTransformFeatureNameUsingDefaultTransformer()
        {
            var transformer = new DefaultFeatureNameTransformer();
            var featureName = "ImportFeature";
            var transformedName = transformer.TransformFeatureName(featureName);
            Assert.AreEqual($"Featurify.{featureName}", transformedName);
        }

        [TestMethod]
        public void CanTransformFeatureNameUsingAsIsTransformer()
        {
            var transformer = new AsIsNameTransformer();
            var featureName = "ImportFeature";
            var transformedName = transformer.TransformFeatureName(featureName);
            Assert.AreEqual(featureName, transformedName);
        }
    }
}

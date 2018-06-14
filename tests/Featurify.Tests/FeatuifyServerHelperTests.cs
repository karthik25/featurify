using Featurify.Contracts;
using Featurify.Exceptions;
using Featurify.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Featurify.Tests
{
    [TestClass]
    public class FeatuifyServerHelperTests
    {
        [TestMethod]
        public void CanIdentifyFeatureStatusWithoutOptions()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "*"
            };
            var result = metadata.IsFeatureEnabled(currentUserId);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithoutOptionsSpecificUser()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var result = metadata.IsFeatureEnabled(currentUserId);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithoutOptionsSpecificUserTurnedOff()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = false,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var result = metadata.IsFeatureEnabled(currentUserId);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithoutOptionsSpecificUserNegative()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4e";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var result = metadata.IsFeatureEnabled(currentUserId);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithOptionsCatchAllNotStrict()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "?"
            };
            var options = new FeaturifyOptions
            {
                AnyUserVerifier = "?",
                UseStrict = false
            };
            var result = metadata.IsFeatureEnabled(currentUserId, options);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithOptionsSpecificUserNotStrict()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var options = new FeaturifyOptions
            {
                AnyUserVerifier = "?",
                UseStrict = false
            };
            var result = metadata.IsFeatureEnabled(currentUserId, options);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanIdentifyFeatureStatusWithOptionsSpecificUserNotStrictTurnedOff()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = false,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var options = new FeaturifyOptions
            {
                AnyUserVerifier = "?",
                UseStrict = false
            };
            var result = metadata.IsFeatureEnabled(currentUserId, options);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FeatureNotEnabledException))]
        public void CanIdentifyFeatureStatusWithOptionsNonExistentUserStrict()
        {
            var currentUserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4e";
            var metadata = new ToggleMetadata
            {
                Name = "Featurify.ImportFeature",
                Value = true,
                UserId = "afefebe0-9478-4ba0-90eb-35d6ac5b9d4f"
            };
            var options = new FeaturifyOptions
            {
                AnyUserVerifier = "?",
                UseStrict = true
            };
            var result = metadata.IsFeatureEnabled(currentUserId, options);
        }
    }

    public class ToggleMetadata : IToggleMetadata
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public string UserId { get; set; }
    }
}

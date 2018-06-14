using Featurify.Contracts;
using System.Threading.Tasks;

namespace Featurify.Demo.Infrastructure
{
    public class DemoAppFeatureMetadataFinder : IToggleMetadataFinder
    {
        public async Task<IToggleMetadata> FindToggleStatus(string featureName, string userId)
        {
            await Task.CompletedTask;
            var metadata = new Toggle
            {
                Name = featureName,
                Value = featureName.Contains("ImportFeature") ? true : false,
                UserId = "?"
            };
            return metadata;
        }
    }

    public class Toggle : IToggleMetadata
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public string UserId { get; set; }
    }
}

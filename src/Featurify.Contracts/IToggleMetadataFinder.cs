using System.Threading.Tasks;

namespace Featurify.Contracts
{
    public interface IToggleMetadataFinder
    {
        Task<IToggleMetadata> FindToggleStatus(string featureName, string userId);
    }
}

using System.Threading.Tasks;

namespace Featurify.Contracts
{
    public interface IUserInfoStrategy
    {
        Task<string> GetCurrentUserId();
    }
}

using Featurify.Contracts;
using System.Threading.Tasks;

namespace Featurify.Demo.Infrastructure
{
    public class DemoAppUserFinderStrategy : IUserInfoStrategy
    {
        public async Task<string> GetCurrentUserId()
        {
            await Task.CompletedTask;
            return "b0486d0f-9114-41a7-a095-e4e92201a41e";
        }
    }
}

using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Application.Common.Models;

namespace Sitanshu.Blogs.Infrastructure.Identity
{
    internal class IdentityService : IIdentityService
    {
        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            return Task.FromResult(true);
        }

        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            return Task.FromResult((Result.Success(), "Sitanshu"));
        }

        public Task<Result> DeleteUserAsync(string userId)
        {
            return Task.FromResult(Result.Success());
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            return Task.FromResult("Sitanshu");
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            return Task.FromResult(true);
        }
    }
}

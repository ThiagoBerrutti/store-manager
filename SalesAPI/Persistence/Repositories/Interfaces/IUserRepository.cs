using SalesAPI.Identity;
using SalesAPI.Models;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByUserName(string userName);
        public Task<User> GetCurrentUser();
    }
}
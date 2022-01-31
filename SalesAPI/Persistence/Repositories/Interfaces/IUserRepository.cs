using SalesAPI.Models;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByUserName(string userName);
    }
}
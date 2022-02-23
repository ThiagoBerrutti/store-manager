using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        public Task<int> CompleteAsync();
    }
}
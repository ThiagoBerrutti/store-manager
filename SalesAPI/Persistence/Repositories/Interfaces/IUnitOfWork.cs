using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        public Task<int> CompleteAsync();
    }
}
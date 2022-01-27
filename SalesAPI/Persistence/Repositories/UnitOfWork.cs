using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SalesDbContext _context;

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
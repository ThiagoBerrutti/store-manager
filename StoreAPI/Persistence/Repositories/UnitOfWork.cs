using StoreAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace StoreAPI.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new InfrastructureException()
                    .SetTitle("Error saving data")
                    .SetDetail(e.Message);
            }
        }
    }
}
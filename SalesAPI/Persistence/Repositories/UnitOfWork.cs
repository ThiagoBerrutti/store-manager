using SalesAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _context;

        public UnitOfWork(SalesDbContext context)
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
                throw new InfrastructureException("Error during save", e);
            }
        }
    }
}
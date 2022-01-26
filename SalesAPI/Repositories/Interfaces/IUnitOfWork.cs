using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repositories
{
    public interface IUnitOfWork
    {
        public Task CompleteAsync();
    }
}

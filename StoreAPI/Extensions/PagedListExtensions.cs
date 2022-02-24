using Microsoft.EntityFrameworkCore;
using StoreAPI.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Extensions
{
    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
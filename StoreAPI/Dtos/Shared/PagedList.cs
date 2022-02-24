using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Dtos
{
    public class PagedList<T> 
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int currentPage, int pageSize, int totalCount) 
        {
            Items = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(1.0 * totalCount / pageSize);
            TotalCount = totalCount;
        }


        public string GetMetadata()
        {
            var metadata = new
            {
                TotalCount,
                PageSize,
                CurrentPage,
                TotalPages,
                HasNext,
                HasPrevious
            };

            var json = JsonConvert.SerializeObject(metadata);

            return json;
        }


        public async static Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}

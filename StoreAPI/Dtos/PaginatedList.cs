using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StoreAPI.Dtos
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginatedList(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            Items = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
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
    }
}
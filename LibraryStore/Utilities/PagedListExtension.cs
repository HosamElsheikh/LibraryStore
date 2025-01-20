using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Utilities
{
    public static class PagedListExtension
    {
        public static async Task<PaginationResult<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var count = query.Count();
            var pagesCount = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginationResult<T>
            {
                Pagination = new PaginationData
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    ItemsCount = count,
                    PagesCount = pagesCount
                },
                Items = items
            };
        }
    }

    public class PaginationResult<T>
    {
        public PaginationData Pagination { get; set; } = new();

        public List<T> Items { get; set; } = new();
    }

    public class PaginationData
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
        public int ItemsCount { get; set; }
    }
}

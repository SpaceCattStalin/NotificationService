using Microsoft.EntityFrameworkCore;

namespace Domain.Common
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; private set; }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        public PaginatedList(IEnumerable<T> items, int count, int index, int pageSize)
        {
            TotalItems = count;
            CurrentPage = index;
            PageSize = pageSize > 100 ? 100 : pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            Items = items ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class with default pagination values.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        public PaginatedList(IEnumerable<T> items, int count)
        {
            TotalItems = count;
            CurrentPage = 1;
            PageSize = 0;
            TotalPages = 1;
            Items = items ?? Enumerable.Empty<T>();
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public static async Task<PaginatedList<T>> GetDataAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PaginatedList<T>> GetDataAsync(IQueryable<T> source)
        {
            int count = await source.CountAsync();
            IEnumerable<T> items = source;
            return new PaginatedList<T>(items, count)
            {
                CurrentPage = 1,
                PageSize = 0,
                TotalPages = 1
            };
        }
    }
}

namespace FinalProjectMvc.Helpers
{
    public static class PaginationExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var totalItems = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<T>(items, page, totalItems, pageSize);
        }

        public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> items, int page, int pageSize, int totalItems)
        {
            return new PagedResult<T>(items, page, totalItems, pageSize);
        }
    }
}

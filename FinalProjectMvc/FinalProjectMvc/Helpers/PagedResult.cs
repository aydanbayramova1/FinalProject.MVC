namespace FinalProjectMvc.Helpers
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedResult(IEnumerable<T> items, int currentPage, int totalItems, int pageSize)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalItems = totalItems;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
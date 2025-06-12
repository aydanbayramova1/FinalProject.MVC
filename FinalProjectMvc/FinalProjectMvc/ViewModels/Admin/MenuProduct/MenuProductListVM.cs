using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.MenuProduct
{
    public class MenuProductListVM
    {
        public IEnumerable<MenuProductVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
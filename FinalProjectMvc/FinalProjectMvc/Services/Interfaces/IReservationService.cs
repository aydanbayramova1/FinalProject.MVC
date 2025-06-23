using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using FinalProjectMvc.ViewModels.Admin.Product;
using FinalProjectMvc.ViewModels.Admin.ProductSize;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using FinalProjectMvc.ViewModels.Admin.Table;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationVM>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReservationCreateVM vm);
        Task<bool> ApproveAsync(int id);
        Task<bool> RejectAsync(int id);
        Task CheckAndDeleteExpiredReservationsAsync();
        Task<List<ProductWithSizeVM>> GetMenuProductsAsync();
        Task<List<TableVM>> GetAllTablesAsync();
        Task<List<CategoryVM>> GetAllCategoriesAsync();
        Task<List<ProductWithSizeVM>> GetLastProductsPerCategoryAsync();
       Task<Table?> GetAvailableTableAsync(int guestCount, DateTime date, TimeSpan time);
        Task<List<OpeningHourVM>> GetOpeningHoursAsync();
    }
}

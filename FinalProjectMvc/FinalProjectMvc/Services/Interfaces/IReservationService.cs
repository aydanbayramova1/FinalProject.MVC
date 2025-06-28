using FinalProjectMvc.Helpers.Enums;
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
        Task<List<ReservationVM>> GetAllReservationsAsync();
        Task<ReservationDetailVM> GetReservationByIdAsync(int id);
        Task<ReservationResultVM> CreateReservationAsync(ReservationCreateVM model);
        Task<bool> UpdateReservationStatusAsync(int id, ReservationStatus status);
        Task<bool> DeleteReservationAsync(int id);
        Task<List<Table>> GetAvailableTablesAsync(DateTime date, TimeSpan timeFrom, TimeSpan timeTo, int guestCount);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan timeFrom, TimeSpan timeTo, int? excludeReservationId = null);
        Task<List<OpeningHourVM>> GetOpeningHoursAsync();
        Task<List<TableVM>> GetAllTablesAsync();
    }
}

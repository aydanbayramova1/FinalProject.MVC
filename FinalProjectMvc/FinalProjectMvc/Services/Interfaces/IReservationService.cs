using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Reservation;

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
    }
}

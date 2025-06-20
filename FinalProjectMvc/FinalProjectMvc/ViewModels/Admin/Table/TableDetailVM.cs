using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.ViewModels.Admin.Reservation;

namespace FinalProjectMvc.ViewModels.Admin.Table
{
    public class TableDetailVM
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<ReservationVM> Reservations { get; set; }
    }
}

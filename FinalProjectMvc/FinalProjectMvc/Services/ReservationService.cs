using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Enums;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using FinalProjectMvc.ViewModels.Admin.Table;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ReservationService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<List<ReservationVM>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Table)
                .Select(r => new ReservationVM
                {
                    Id = r.Id,
                    Fullname = r.Fullname,
                    Email = r.Email,
                    Phone = r.Phone,
                    Date = r.Date,
                    TimeFrom = r.TimeFrom,
                    TimeTo = r.TimeTo,
                    GuestCount = r.GuestCount,
                    TableInfo = $"Table {r.Table.Number} ({r.Table.Location}) - {r.Table.Capacity} seats",
                    Status = r.Status
                })
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }

        public async Task<ReservationDetailVM> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return null;

            return new ReservationDetailVM
            {
                Id = reservation.Id,
                Fullname = reservation.Fullname,
                Email = reservation.Email,
                Phone = reservation.Phone,
                Date = reservation.Date,
                TimeFrom = reservation.TimeFrom,
                TimeTo = reservation.TimeTo,
                GuestCount = reservation.GuestCount,
                TableId = reservation.TableId,
                TableNumber = reservation.Table.Number.ToString(),
                TableCapacity = reservation.Table.Capacity,
                TableLocation = reservation.Table.Location,
                Status = reservation.Status,
                CreatedAt = reservation.CreateDate
            };
        }

        public async Task<bool> CreateReservationAsync(ReservationCreateVM model)
        {
            try
            {
                var now = DateTime.Now;

                if (model.Date.Date < now.Date ||
                    (model.Date.Date == now.Date && model.TimeFrom <= now.TimeOfDay))
                {
                    return false;
                }

                var existingReservation = await _context.Reservations
       .Where(r => r.TableId == model.TableId &&
                  r.Date.Date == model.Date.Date &&
                  ((model.TimeFrom >= r.TimeFrom && model.TimeFrom < r.TimeTo) ||
                   (model.TimeTo > r.TimeFrom && model.TimeTo <= r.TimeTo) ||
                   (model.TimeFrom <= r.TimeFrom && model.TimeTo >= r.TimeTo)))
       .FirstOrDefaultAsync();

                if (existingReservation != null)
                {
                    return false; // Masa artıq rezerv edilib
                }

                if (!await IsTableAvailableAsync(model.TableId, model.Date, model.TimeFrom, model.TimeTo))
                    return false;

                var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == model.TableId);
                if (table == null)
                    return false;

                if (model.Guests != table.Capacity && model.Guests != table.Capacity - 1)
                    return false;

                var reservation = new Reservation
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Phone = model.Phone,
                    Date = model.Date,
                    TimeFrom = model.TimeFrom,
                    TimeTo = model.TimeTo,
                    GuestCount = model.Guests,
                    TableId = model.TableId,
                    Status = ReservationStatus.Pending,
                    CreateDate = DateTime.UtcNow
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                await SendReservationConfirmationEmailAsync(reservation);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateReservationStatusAsync(int id, ReservationStatus status)
        {
            try
            {
                var reservation = await _context.Reservations.Include(r => r.Table).FirstOrDefaultAsync(r => r.Id == id);
                if (reservation == null) return false;

                var oldStatus = reservation.Status;
                reservation.Status = status;

                await _context.SaveChangesAsync();

                if (oldStatus != status)
                    await SendStatusChangeEmailAsync(reservation, status);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(id);
                if (reservation == null) return false;

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Table>> GetAvailableTablesAsync(DateTime date, TimeSpan timeFrom, TimeSpan timeTo, int guestCount)
        {
            var unavailableTableIds = await _context.Reservations
                .Where(r => r.Date == date &&
                            r.Status != ReservationStatus.Rejected &&
                            (r.TimeFrom < timeTo && r.TimeTo > timeFrom))
                .Select(r => r.TableId)
                .ToListAsync();

            return await _context.Tables
                .Where(t => !unavailableTableIds.Contains(t.Id) && t.Capacity >= guestCount)
                .ToListAsync();
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan timeFrom, TimeSpan timeTo, int? excludeReservationId = null)
        {
            return !await _context.Reservations
                .AnyAsync(r =>
                    r.TableId == tableId &&
                    r.Date == date &&
                    r.Status != ReservationStatus.Rejected &&
                    (excludeReservationId == null || r.Id != excludeReservationId) &&
                    r.TimeFrom < timeTo && r.TimeTo > timeFrom);
        }
        public async Task<List<TableVM>> GetAllTablesAsync()
        {
            return await _context.Tables
                .Select(t => new TableVM
                {
                    Id = t.Id,
                    Number = t.Number,
                    Capacity = t.Capacity,
                    Location = t.Location
                }).ToListAsync();
        }
        public async Task<List<OpeningHourVM>> GetOpeningHoursAsync()
        {
            return await _context.OpeningHours
                .Select(x => new OpeningHourVM
                {
                    DayRange = x.DayRange,
                    TimeRange = x.TimeRange
                })
                .ToListAsync();
        }

        private async Task SendReservationConfirmationEmailAsync(Reservation reservation)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == reservation.TableId);
            string subject = "Reservation Confirmed – Caffe Luna";
            string body = $@"
    <div style='font-family:Arial, sans-serif; color:#333; line-height:1.6;'>
        <h2 style='color:#2c3e50;'>Dear {reservation.Fullname},</h2>
        <p>We are pleased to inform you that your reservation has been successfully received and is currently awaiting approval by our team.</p>
        <p>
            <strong>Date:</strong> {reservation.Date:yyyy-MM-dd}<br />
            <strong>Time:</strong> {reservation.TimeFrom} - {reservation.TimeTo}<br />
            <strong>Table:</strong> {table?.Number} ({table?.Location})
        </p>
        <p>We’ll be in touch with you shortly to confirm the final details.</p>
        <p>Thank you for choosing <strong>Caffe Luna</strong>!</p>
    </div>";

            await _emailService.SendAsync(reservation.Email, subject, body);
        }

        private async Task SendStatusChangeEmailAsync(Reservation reservation, ReservationStatus status)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == reservation.TableId);
            string subject = "";
            string message = "";

            if (status == ReservationStatus.Approved)
            {
                subject = "Your Reservation Has Been Approved!";
                message = $@"
<h3>Dear {reservation.Fullname},</h3>
<p>Your reservation has been approved!</p>
<p><b>Date:</b> {reservation.Date:yyyy-MM-dd}</p>
<p><b>Time:</b> {reservation.TimeFrom} - {reservation.TimeTo}</p>
<p><b>Table:</b> {table?.Number} ({table?.Location})</p>
<p>Caffe Luna is waiting for you!</p>";
            }
            else if (status == ReservationStatus.Rejected)
            {
                subject = "Your Reservation Has Been Rejected";
                message = $@"
<h3>Dear {reservation.Fullname},</h3>
<p>Unfortunately, your reservation has been declined.</p>
<p>Please try again at a different date and time.</p>";
            }
            if (!string.IsNullOrWhiteSpace(subject))
                await _emailService.SendAsync(reservation.Email, subject, message);
        }
    }
}

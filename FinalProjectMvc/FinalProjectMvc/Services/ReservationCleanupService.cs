using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProjectMvc.Services
{
    public class ReservationCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationCleanupService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(30);

        public ReservationCleanupService(
            IServiceProvider serviceProvider,
            ILogger<ReservationCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupReservationsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error during reservation cleanup at {DateTime.UtcNow}");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task CleanupReservationsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var now = DateTime.UtcNow;
            var oneHourAgo = now.AddHours(-1);

            var rejectedToDelete = await context.Reservations
                .Where(r => r.Status == ReservationStatus.Rejected &&
                            r.CreateDate <= oneHourAgo)
                .ToListAsync();

            var approvedExpired = await context.Reservations
                .Where(r => r.Status == ReservationStatus.Approved &&
                            r.Date < now.Date)
                .ToListAsync();

            if (rejectedToDelete.Any() || approvedExpired.Any())
            {
                context.Reservations.RemoveRange(rejectedToDelete);
                context.Reservations.RemoveRange(approvedExpired);
                await context.SaveChangesAsync();

                _logger.LogInformation($"[{DateTime.UtcNow}] Deleted {rejectedToDelete.Count} rejected and {approvedExpired.Count} expired approved reservations.");
            }
        }
    }
}

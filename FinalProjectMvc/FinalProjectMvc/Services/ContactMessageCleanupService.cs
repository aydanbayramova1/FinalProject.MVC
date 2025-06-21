using FinalProjectMvc.Services.Interfaces;

namespace FinalProjectMvc.Services
{
    public class ContactMessageCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ContactMessageCleanupService> _logger;

        public ContactMessageCleanupService(IServiceProvider serviceProvider, ILogger<ContactMessageCleanupService> logger)
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
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var contactMessageService = scope.ServiceProvider.GetRequiredService<IContactMessageService>();
                        await contactMessageService.CleanupOldRepliedMessagesAsync();
                    }

                    _logger.LogInformation("Contact message cleanup completed at: {time}", DateTimeOffset.Now);

                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing contact message cleanup");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }
    }
}


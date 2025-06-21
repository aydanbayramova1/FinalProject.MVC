using System.Diagnostics;
using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ContactMessageService(AppDbContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task CreateAsync(ContactMessageCreateVM vm)
        {
            var entity = _mapper.Map<ContactMessage>(vm);
            entity.CreateDate = DateTime.Now;
            Debug.WriteLine($"New Message: {entity.FirstName} {entity.LastName}");
            await _context.ContactMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactMessageVM>> GetAllAsync()
        {
            await DeleteOldRepliedMessagesAsync();

            var messages = await _context.ContactMessages
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ContactMessageVM>>(messages);
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message is not null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ContactMessageVM> GetByIdAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            return _mapper.Map<ContactMessageVM>(message);
        }

        public async Task ReplyToMessageAsync(ContactMessageReplyVM vm)
        {
            var message = await _context.ContactMessages.FindAsync(vm.Id);
            if (message != null)
            {
                message.Reply = vm.Reply;
                message.ReplyDate = DateTime.Now;
                message.IsReplied = true;
                await _context.SaveChangesAsync();

                var subject = "Re: Your Contact Message";
                var htmlContent = $@"
                <h2>Thank you for contacting us!</h2>
                <p>Dear {message.FirstName} {message.LastName},</p>
                <p>Thank you for your message. Here is our response:</p>
                <div style='background-color: #f8f9fa; padding: 15px; border-left: 4px solid #007bff; margin: 20px 0;'>
                    <strong>Your original message:</strong><br>
                    {message.Message}
                </div>
                <div style='background-color: #e8f5e8; padding: 15px; border-left: 4px solid #28a745; margin: 20px 0;'>
                    <strong>Our response:</strong><br>
                    {vm.Reply}
                </div>
                <p>Best regards,<br>Support Team</p>
            ";
                await _emailService.SendAsync(message.Email, subject, htmlContent);
            }
        }

        private async Task DeleteOldRepliedMessagesAsync()
        {
            var twoHoursAgo = DateTime.Now.AddHours(-2);

            var oldRepliedMessages = await _context.ContactMessages
                .Where(m => m.IsReplied && m.ReplyDate.HasValue && m.ReplyDate.Value <= twoHoursAgo)
                .ToListAsync();

            if (oldRepliedMessages.Any())
            {
                _context.ContactMessages.RemoveRange(oldRepliedMessages);
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Deleted {oldRepliedMessages.Count} old replied messages");
            }
        }

        public async Task CleanupOldRepliedMessagesAsync()
        {
            await DeleteOldRepliedMessagesAsync();
        }
    }
}
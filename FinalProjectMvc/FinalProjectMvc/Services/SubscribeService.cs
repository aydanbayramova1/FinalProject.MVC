using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public SubscribeService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task AddAsync(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var newsletter = new Subscribe { Email = email };
                _context.Subscribes.Add(newsletter);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribes
                .OrderByDescending(n => n.CreateDate)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var newsletter = await _context.Subscribes.FindAsync(id);
            if (newsletter != null)
            {
                _context.Subscribes.Remove(newsletter);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.Subscribes.AnyAsync(n => n.Email.ToLower() == email.ToLower());
        }

    }
}
    


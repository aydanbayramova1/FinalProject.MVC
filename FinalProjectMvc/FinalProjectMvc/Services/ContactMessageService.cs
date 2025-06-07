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

        public ContactMessageService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(ContactMessageCreateVM vm)
        {
            var entity = _mapper.Map<ContactMessage>(vm);
            entity.CreateDate = DateTime.Now;

            Debug.WriteLine($"📩 New Message: {entity.FirstName} {entity.LastName}");

            await _context.ContactMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactMessageVM>> GetAllAsync()
        {
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
    }
}
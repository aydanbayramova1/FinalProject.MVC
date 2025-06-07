using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactUsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactUsVM>> GetAllAsync()
        {
            var contacts = await _context.ContactUses.ToListAsync();
            return _mapper.Map<IEnumerable<ContactUsVM>>(contacts);
        }

        public async Task<ContactUsDetailVM> GetByIdAsync(int id)
        {
            var contact = await _context.ContactUses.FindAsync(id);
            if (contact == null) throw new KeyNotFoundException("Contact not found");
            return _mapper.Map<ContactUsDetailVM>(contact);
        }

        public async Task CreateAsync(ContactUsCreateVM vm)
        {
            var contact = _mapper.Map<ContactUs>(vm);
            await _context.ContactUses.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ContactUsEditVM vm)
        {
            var contact = await _context.ContactUses.FindAsync(vm.Id);
            if (contact == null) throw new KeyNotFoundException("Contact not found");

            _mapper.Map(vm, contact);
            _context.ContactUses.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _context.ContactUses.FindAsync(id);
            if (contact == null) throw new KeyNotFoundException("Contact not found");

            _context.ContactUses.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

}

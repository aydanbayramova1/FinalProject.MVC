using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMvc.Services
{
    public class ApproachService : IApproachService
    {
        private readonly AppDbContext _context;

        public ApproachService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Approach> GetFirstAsync()
        {
            return await _context.Approaches.FirstOrDefaultAsync();
        }
        public async Task<Approach> GetAsync()
        {
            return await _context.Approaches.Include(x => x.Items).FirstOrDefaultAsync();
        }
        public async Task<Approach> GetByIdAsync(int id)
        {
            return await _context.Approaches.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateAsync(Approach approach)
        {
            _context.Approaches.Add(approach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Approach approach)
        {
            _context.Approaches.Update(approach);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Approaches
                .Include(a => a.Items) 
                .FirstOrDefaultAsync(a => a.Id == id);

            if (entity != null)
            {
                _context.Approaches.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _context.Approaches.AnyAsync();
        }
    }
}

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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ApproachService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<ApproachVM>> GetAllAsync()
        {
            var data = await _context.Approaches.ToListAsync();
            return _mapper.Map<List<ApproachVM>>(data);
        }
        public async Task<ApproachDetailVM> GetByIdAsync(int id)
        {
            var entity = await _context.Approaches
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ApproachDetailVM>(entity);
        }

        public async Task CreateAsync(ApproachCreateVM model)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            string path = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            var entity = _mapper.Map<Approach>(model);
            entity.Image = fileName;

            await _context.Approaches.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApproachEditVM model)
        {
            var entity = await _context.Approaches.FindAsync(model.Id);
            if (entity == null) return;

            entity.Title = model.Title;
            entity.SubTitle = model.SubTitle;

            if (model.ImageFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                string newPath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads", entity.Image);
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                entity.Image = fileName;
            }

            _context.Approaches.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync()
        {
            return await _context.Approaches.AnyAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Approaches.FindAsync(id);
            if (entity == null) return;

            if (!string.IsNullOrEmpty(entity.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads", entity.Image);
                if (File.Exists(path)) File.Delete(path);
            }

            _context.Approaches.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

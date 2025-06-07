using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class OurStoryService : IOurStoryService
    {
        private readonly AppDbContext _context;

        public OurStoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OurStory> GetFirstAsync()
        {
            return await _context.OurStories.FirstOrDefaultAsync();
        }
        public async Task<OurStory> GetAsync()
        {
            return await _context.OurStories
                .Include(x => x.StoryItems)
                .FirstOrDefaultAsync();
        }
        public async Task<OurStory> GetByIdAsync(int id) 
        {
            return await _context.OurStories
                .Include(x => x.StoryItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateAsync(OurStory ourStory)
        {
            _context.OurStories.Add(ourStory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OurStory ourStory)
        {
            _context.OurStories.Update(ourStory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.OurStories
                .Include(s => s.StoryItems) 
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entity != null)
            {
                _context.OurStories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _context.OurStories.AnyAsync();
        }
    }
}

using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class StoryItemService : IStoryItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public StoryItemService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<StoryItemVM>> GetAllAsync()
        {
            var items = await _context.StoryItems.Include(s => s.OurStory).ToListAsync();
            return _mapper.Map<List<StoryItemVM>>(items);
        }

        public async Task<StoryItemDetailVM> GetDetailAsync(int id)
        {
            var item = await _context.StoryItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Story item not found");

            return _mapper.Map<StoryItemDetailVM>(item);
        }

        public async Task CreateAsync(StoryItemCreateVM vm)
        {
            var ourStory = await _context.OurStories.FirstOrDefaultAsync();
            if (ourStory == null) throw new KeyNotFoundException("OurStory must exist before creating a story item");

            var item = _mapper.Map<StoryItem>(vm);
            item.OurStoryId = ourStory.Id;

            if (vm.IconFile != null)
            {
                string fileName = await vm.IconFile.SaveFileAsync(_env.WebRootPath, "uploads/storyicons");
                item.IconImg = fileName;
            }

            await _context.StoryItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<StoryItemEditVM> GetEditAsync(int id)
        {
            var item = await _context.StoryItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Story item not found");

            return _mapper.Map<StoryItemEditVM>(item);
        }

        public async Task EditAsync(StoryItemEditVM vm)
        {
            var item = await _context.StoryItems.FindAsync(vm.Id);
            if (item == null) throw new KeyNotFoundException("Story item not found");

            _mapper.Map(vm, item);

            if (vm.IconFile != null)
            {
                string fileName = await vm.IconFile.SaveFileAsync(_env.WebRootPath, "uploads/storyicons");
                item.IconImg = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.StoryItems.FindAsync(id);
            if (item == null) throw new KeyNotFoundException("Story item not found");

            _context.StoryItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

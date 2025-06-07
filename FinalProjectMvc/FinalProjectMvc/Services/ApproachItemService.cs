using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.EntityFrameworkCore;

public class ApproachItemService :IApproachItemService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public ApproachItemService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }

    public async Task<List<ApproachItemVM>> GetAllAsync()
    {
        var items = await _context.ApproachItems.Include(s => s.Approach).ToListAsync();
        return _mapper.Map<List<ApproachItemVM>>(items);
    }

    public async Task<ApproachItemDetailVM?> GetDetailAsync(int id)
    {
        var item = await _context.ApproachItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return null;

        return _mapper.Map<ApproachItemDetailVM>(item);
    }

    public async Task CreateAsync(ApproachItemCreateVM vm)
    {
        var ourStory = await _context.Approaches.FirstOrDefaultAsync();
        if (ourStory == null) throw new KeyNotFoundException("Approach must exist before creating a story item");

        var item = _mapper.Map<ApproachItem>(vm);
        item.ApproachId = ourStory.Id;

        if (vm.IconFile != null)
        {
            string fileName = await vm.IconFile.SaveFileAsync(_env.WebRootPath, "uploads/approachicons");
            item.IconPath = fileName;
        }

        await _context.ApproachItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<ApproachItemEditVM> GetEditAsync(int id)
    {
        var item = await _context.ApproachItems.FindAsync(id);
        if (item == null) throw new KeyNotFoundException("Approach item not found");

        return _mapper.Map<ApproachItemEditVM>(item);
    }

    public async Task EditAsync(ApproachItemEditVM vm)
    {
        var item = await _context.ApproachItems.FindAsync(vm.Id);
        if (item == null) throw new KeyNotFoundException("Approach item not found");

        _mapper.Map(vm, item);

        if (vm.IconFile != null)
        {
            string fileName = await vm.IconFile.SaveFileAsync(_env.WebRootPath, "uploads/approachicons");
            item.IconPath = fileName;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.ApproachItems.FindAsync(id);
        if (item == null) throw new KeyNotFoundException("Approach item not found");

        _context.ApproachItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}


using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;
using Microsoft.EntityFrameworkCore;

public class ApproachItemService :IApproachItemService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ApproachItemService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ApproachItemDetailVM>> GetAllAsync()
    {
        var data = await _context.ApproachItems.Include(x => x.Approach).ToListAsync();
        return _mapper.Map<List<ApproachItemDetailVM>>(data);
    }

    public async Task<List<ApproachItemDetailVM>> GetByApproachIdAsync(int approachId)
    {
        var data = await _context.ApproachItems
            .Where(x => x.ApproachId == approachId)
            .Include(x => x.Approach)
            .ToListAsync();

        return _mapper.Map<List<ApproachItemDetailVM>>(data);
    }

    public async Task<ApproachItemDetailVM> GetByIdAsync(int id)
    {
        var entity = await _context.ApproachItems
            .Include(x => x.Approach)
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<ApproachItemDetailVM>(entity);
    }

    public async Task CreateAsync(ApproachItemCreateVM model)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.IconFile.FileName);
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "approachItems");
        string fullPath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await model.IconFile.CopyToAsync(stream);
        }

        var entity = new ApproachItem
        {
            Title = model.Title,
            Description = model.Description,
            IconPath = Path.Combine("uploads", "approachItems", fileName),
            ApproachId = model.ApproachId
        };

        await _context.ApproachItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(ApproachItemEditVM model)
    {
        var entity = await _context.ApproachItems.FindAsync(model.Id);
        if (entity == null) return;

        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.IconPath = model.IconPath;
        entity.ApproachId = model.ApproachId;

        _context.ApproachItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.ApproachItems.FindAsync(id);
        if (entity == null) return;

        _context.ApproachItems.Remove(entity);
        await _context.SaveChangesAsync();
    }
}


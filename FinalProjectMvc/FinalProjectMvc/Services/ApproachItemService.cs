using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ApproachItem;

public class ApproachItemService :IApproachItemService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ApproachItemService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApproachItemVM> GetByIdAsync(int id)
    {
        var item = await _context.ApproachItems.FindAsync(id);
        if (item == null) throw new NullReferenceException("Item not found");

        return _mapper.Map<ApproachItemVM>(item);
    }

    public async Task CreateAsync(ApproachItemCreateVM vm)
    {
        var model = _mapper.Map<ApproachItem>(vm);
        await _context.ApproachItems.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ApproachItemEditVM vm)
    {
        var item = await _context.ApproachItems.FindAsync(id);
        if (item == null) throw new NullReferenceException("Item not found");

        _mapper.Map(vm, item);
        _context.ApproachItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.ApproachItems.FindAsync(id);
        if (item == null) throw new NullReferenceException("Item not found");

        _context.ApproachItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}

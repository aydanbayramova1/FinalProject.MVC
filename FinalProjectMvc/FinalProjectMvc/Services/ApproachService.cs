using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.EntityFrameworkCore;

public class ApproachService : IApproachService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ApproachService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ApproachVM>> GetAsync()
    {
        // Fetch all approaches, including their associated items
        var approaches = await _context.Approaches.Include(x => x.Items).ToListAsync();

        // Map the result to a list of ApproachVMs
        return _mapper.Map<IEnumerable<ApproachVM>>(approaches);
    }


    public async Task<ApproachVM> GetByIdAsync(int id) 
    {
        var approach = await _context.Approaches.Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (approach == null) throw new NullReferenceException("Approach not found");

        return _mapper.Map<ApproachVM>(approach);
    }

    public async Task CreateAsync(ApproachCreateVM vm)
    {
        if (await _context.Approaches.AnyAsync())
            throw new InvalidOperationException("An Approach already exists.");

        var model = _mapper.Map<Approach>(vm);
        await _context.Approaches.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ApproachEditVM vm)
    {
        var model = await _context.Approaches.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) throw new NullReferenceException("Approach not found");

        _mapper.Map(vm, model);
        _context.Approaches.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _context.Approaches.FindAsync(id);
        if (model == null) throw new NullReferenceException("Approach not found");

        _context.Approaches.Remove(model);
        await _context.SaveChangesAsync();
    }
}

using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.EntityFrameworkCore;

public class ApproachService : IApproachService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper; // AutoMapper istifadə olunur deyə varsayıram

    public ApproachService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ApproachVM>> GetAsync()
    {
        var approaches = await _context.Approaches.Include(a => a.Items).ToListAsync();
        return _mapper.Map<IEnumerable<ApproachVM>>(approaches);
    }

    public async Task<ApproachVM> GetByIdAsync(int id)
    {
        var approach = await _context.Approaches.Include(a => a.Items)
                            .FirstOrDefaultAsync(a => a.Id == id);
        if (approach == null)
            return null;

        return _mapper.Map<ApproachVM>(approach);
    }

    public async Task CreateAsync(ApproachCreateVM vm)
    {
        var approach = _mapper.Map<Approach>(vm);

        // ImageFile-ni serverə yükləmə kodunu buraya əlavə etməlisən
        // Məsələn: approach.Image = await _imageService.UploadAsync(vm.ImageFile);

        _context.Approaches.Add(approach);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ApproachEditVM vm)
    {
        var approach = await _context.Approaches.FindAsync(id);
        if (approach == null) throw new Exception("Approach not found");

        // Update sahələr
        approach.Title = vm.Title;
        approach.SubTitle = vm.SubTitle;

        if (vm.ImageFile != null)
        {
            // Serverə yeni şəkil yüklə və köhnəsini sil (əgər varsa)
            // approach.Image = await _imageService.UploadAsync(vm.ImageFile);
        }

        _context.Approaches.Update(approach);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var approach = await _context.Approaches.FindAsync(id);
        if (approach == null) throw new Exception("Approach not found");

        _context.Approaches.Remove(approach);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> HasAnyAsync()
    {
        return await _context.Approaches.AnyAsync();
    }
}

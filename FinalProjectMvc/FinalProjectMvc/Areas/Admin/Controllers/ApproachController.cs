using AutoMapper;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Approach;
using Microsoft.AspNetCore.Mvc;

public class ApproachController : Controller
{
    private readonly IApproachService _approachService;
    private readonly IApproachItemService _approachItemService;
    private readonly IMapper _mapper;

    public ApproachController(IApproachService approachService, IApproachItemService approachItemService, IMapper mapper)
    {
        _approachService = approachService;
        _approachItemService = approachItemService;
        _mapper = mapper;
    }

    // GET: Approach/Index
    public async Task<IActionResult> Index()
    {
        var approaches = await _approachService.GetAsync();
        return View(approaches);
    }

    // GET: Approach/Details/5
    //public async Task<IActionResult> Details(int id)
    //{
    //    var approach = await _approachService.GetByIdAsync(id);
    //    if (approach == null) return NotFound();

    //    var items = await _approachItemService.GetByIdAsync(id);
    //    var vm = new ApproachDetailsVM
    //    {
    //        Id = approach.Id,
    //        Title = approach.Title,
    //        SubTitle = approach.SubTitle,
    //        Image = approach.Image,
    //        Items = items.ToList()
    //    };

    //    return View(vm);
    //}

    // GET: Approach/Create
    public async Task<IActionResult> Create()
    {
        bool hasApproach = await _approachService.HasAnyAsync();
        if (hasApproach)
            return RedirectToAction(nameof(Index));

        return View();
    }

    // POST: Approach/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApproachCreateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _approachService.CreateAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    // GET: Approach/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var approach = await _approachService.GetByIdAsync(id);
        if (approach == null) return NotFound();

        var editVm = _mapper.Map<ApproachEditVM>(approach);
        return View(editVm);
    }

    // POST: Approach/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ApproachEditVM vm)
    {
        if (id != vm.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(vm);

        try
        {
            await _approachService.UpdateAsync(id, vm);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: Approach/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _approachService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return RedirectToAction(nameof(Index));
    }
}

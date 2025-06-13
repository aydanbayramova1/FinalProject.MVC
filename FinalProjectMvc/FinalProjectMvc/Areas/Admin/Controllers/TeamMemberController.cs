using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.TeamMember;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var members = await _teamMemberService.GetAllAsync();
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemberCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _teamMemberService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _teamMemberService.GetByIdAsync(id);
            if (member == null) return NotFound();

            var vm = new TeamMemberEditVM
            {
                FullName = member.FullName,
                Position = member.Position,
                Description = member.Description,
                ImgUrl = member.Img,
            };

            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamMemberEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _teamMemberService.EditAsync(id, vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var member = await _teamMemberService.GetByIdAsync(id);
            if (member == null) return NotFound();

            var vm = new TeamMemberDetailVM
            {
                FullName = member.FullName,
                Position = member.Position,
                Description = member.Description,
                ImgUrl = member.Img,
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamMemberService.DeleteAsync(id);
            return Ok();
        }

    }
}

using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Subscribe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        public async Task<IActionResult> Index()
        {
            var newsletters = await _subscribeService.GetAllAsync();

            var viewModel = new SubscribeListVM
            {
                Items = newsletters.Select(n => new SubscribeVM
                {
                    Id = n.Id,
                    Email = n.Email,
                    CreateDate = n.CreateDate
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


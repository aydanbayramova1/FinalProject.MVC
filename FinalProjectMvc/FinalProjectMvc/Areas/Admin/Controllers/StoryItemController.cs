using FinalProjectMvc.Models;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.StoryItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoryItemController : Controller
    {
        private readonly IStoryItemService _storyItemService;
        private readonly IOurStoryService _ourStoryService;
        public StoryItemController(IStoryItemService storyItemService, IOurStoryService ourStoryService)
        {
            _storyItemService = storyItemService;
            _ourStoryService = ourStoryService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var items = await _storyItemService.GetAllAsync();
            return View(items);
        }



        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var ourStory = _ourStoryService.GetFirstAsync(); 

            if (ourStory == null)
            {
                return RedirectToAction("Index", "OurStory");
            }

            var vm = new StoryItemCreateVM
            {
                OurStoryId = ourStory.Id
            };

            return View(vm); 
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(StoryItemCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _storyItemService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _storyItemService.GetEditAsync(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(StoryItemEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _storyItemService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var item = await _storyItemService.GetDetailAsync(id);
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _storyItemService.DeleteAsync(id);
            return Ok();
        }

    }
}

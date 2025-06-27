//using FinalProjectMvc.Data;
//using FinalProjectMvc.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FinalProjectMvc.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize(Roles = "SuperAdmin,Admin")]
//    public class MessageController : Controller
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly AppDbContext _context;

//        public MessageController(UserManager<AppUser> userManager, AppDbContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }

//        public async Task<IActionResult> Inbox()
//        {
//            var currentUser = await _userManager.GetUserAsync(User);
//            var messages = await _context.Messages
//                .Where(m => m.ReceiverId == currentUser.Id)
//                .Include(m => m.Sender)
//                .OrderByDescending(m => m.CreateDate)
//                .ToListAsync();

//            return View(messages);
//        }

//        public async Task<IActionResult> Send()
//        {
//            var users = await _userManager.GetUsersInRoleAsync("Admin");
//            return View(users);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Send(string receiverId, string content)
//        {
//            var sender = await _userManager.GetUserAsync(User);

//            if (string.IsNullOrWhiteSpace(content)) return BadRequest();

//            Message msg = new Message
//            {
//                SenderId = sender.Id,
//                ReceiverId = receiverId,
//                Content = content,
//                CreateDate = DateTime.Now
//            };

//            _context.Messages.Add(msg);
//            await _context.SaveChangesAsync();

//            return RedirectToAction("Inbox");
//        }
//    }
//}

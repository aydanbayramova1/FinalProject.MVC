using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.IntroVideo;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class IntroVideoService : IIntroVideoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IntroVideoService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IntroVideo> GetAsync()
        {
            return await _context.IntroVideos.Include(iv => iv.Counters).FirstOrDefaultAsync();
        }
        public async Task<List<IntroVideo>> GetAllAsync()
        {
            return await _context.IntroVideos.ToListAsync();
        }

        public async Task CreateAsync(IntroVideoCreateVM vm)
        {
            string videoFileName = await SaveFileAsync(vm.VideoFile, "videos");
            string imgFileName = await SaveFileAsync(vm.ImgFile, "images");

            IntroVideo video = new IntroVideo
            {
                Title = vm.Title,
                Subtitle = vm.Subtitle,
                VideoUrl = videoFileName,
                Img = imgFileName
            };

            await _context.IntroVideos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task<IntroVideoEditVM> GetEditVMAsync(int id)
        {
            var video = await _context.IntroVideos.FindAsync(id);
            if (video == null) return null;

            return new IntroVideoEditVM
            {
                Id = video.Id,
                Title = video.Title,
                Subtitle = video.Subtitle,
                ExistingVideoUrl = video.VideoUrl,
                ExistingImgPath = video.Img
            };
        }

        public async Task EditAsync(IntroVideoEditVM vm)
        {
            var video = await _context.IntroVideos.FindAsync(vm.Id);
            if (video == null) return;

            video.Title = vm.Title;
            video.Subtitle = vm.Subtitle;

            if (vm.NewVideoFile != null)
            {
                string videoPath = await SaveFileAsync(vm.NewVideoFile, "videos");
                video.VideoUrl = videoPath;
            }

            if (vm.NewImgFile != null)
            {
                string imgPath = await SaveFileAsync(vm.NewImgFile, "images");
                video.Img = imgPath;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var video = await _context.IntroVideos.FindAsync(id);
            if (video != null)
            {
                _context.IntroVideos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", folder);

         
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string path = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

    }
}

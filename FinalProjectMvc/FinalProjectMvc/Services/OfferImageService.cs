using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OfferImage;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class OfferImageService : IOfferImageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public OfferImageService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<OurOffer?> GetOurOfferAsync()
        {
            return await _context.OurOffers.FirstOrDefaultAsync();
        }
        public async Task<List<OfferImageVM>> GetAllAsync()
        {
            var offer = await _context.OurOffers.FirstOrDefaultAsync();
            if (offer == null) return new List<OfferImageVM>();

            var images = await _context.OfferImages
                .Where(x => x.OurOfferId == offer.Id)
                .ToListAsync();

            if (images.Count == 0)
                return new List<OfferImageVM>();

            var vm = new OfferImageVM
            {
                OurOfferId = offer.Id,
                MainImageUrl = images.FirstOrDefault(x => x.ImageType == "Main")?.ImageUrl,
                LeftImageUrl = images.FirstOrDefault(x => x.ImageType == "Left")?.ImageUrl,
                RightImageUrl = images.FirstOrDefault(x => x.ImageType == "Right")?.ImageUrl
            };

            return new List<OfferImageVM> { vm };
        }


        public async Task<OfferImageDetailVM?> GetDetailAsync()
        {
            var offer = await _context.OurOffers.FirstOrDefaultAsync();
            var images = await _context.OfferImages
                .Where(x => x.OurOfferId == offer.Id)
                .ToListAsync();

            return new OfferImageDetailVM
            {
                OurOfferId = offer.Id,
                MainImageUrl = images.FirstOrDefault(x => x.ImageType == "Main")?.ImageUrl,
                LeftImageUrl = images.FirstOrDefault(x => x.ImageType == "Left")?.ImageUrl,
                RightImageUrl = images.FirstOrDefault(x => x.ImageType == "Right")?.ImageUrl
            };
        }

        public async Task<OfferImageDetailVM?> GetImageDetailByIdAsync(int id)
        {
            var image = await _context.OfferImages.FirstOrDefaultAsync(x => x.Id == id);
            if (image == null) return null;

            var images = await _context.OfferImages
                .Where(x => x.OurOfferId == image.OurOfferId)
                .ToListAsync();

            return new OfferImageDetailVM
            {
                OurOfferId = image.OurOfferId,
                MainImageUrl = images.FirstOrDefault(x => x.ImageType == "Main")?.ImageUrl,
                LeftImageUrl = images.FirstOrDefault(x => x.ImageType == "Left")?.ImageUrl,
                RightImageUrl = images.FirstOrDefault(x => x.ImageType == "Right")?.ImageUrl
            };
        }

        public async Task<OfferImage?> GetByIdAsync(int id)
        {
            return await _context.OfferImages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OfferImageEditVM?> GetEditAsync()
        {
            var offer = await _context.OurOffers.FirstOrDefaultAsync();
            var images = await _context.OfferImages
                .Where(x => x.OurOfferId == offer.Id)
                .ToListAsync();

            return new OfferImageEditVM
            {
                OurOfferId = offer.Id,
                MainImageUrl = images.FirstOrDefault(x => x.ImageType == "Main")?.ImageUrl,
                LeftImageUrl = images.FirstOrDefault(x => x.ImageType == "Left")?.ImageUrl,
                RightImageUrl = images.FirstOrDefault(x => x.ImageType == "Right")?.ImageUrl
            };
        }

        public async Task CreateAsync(OfferImageCreateVM vm)
        {
            var offer = await _context.OurOffers.FirstOrDefaultAsync();
            if (offer == null) throw new Exception("OurOffer tapılmadı.");

            if (vm.MainImage != null)
            {
                string fileName = await SaveImageAsync(vm.MainImage, "Main", offer.Id);
                _context.OfferImages.Add(new OfferImage
                {
                    OurOfferId = offer.Id,
                    ImageType = "Main",
                    ImageUrl = fileName
                });
            }

            if (vm.LeftImage != null)
            {
                string fileName = await SaveImageAsync(vm.LeftImage, "Left", offer.Id);
                _context.OfferImages.Add(new OfferImage
                {
                    OurOfferId = offer.Id,
                    ImageType = "Left",
                    ImageUrl = fileName
                });
            }

            if (vm.RightImage != null)
            {
                string fileName = await SaveImageAsync(vm.RightImage, "Right", offer.Id);
                _context.OfferImages.Add(new OfferImage
                {
                    OurOfferId = offer.Id,
                    ImageType = "Right",
                    ImageUrl = fileName
                });
            }

            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(OfferImageEditVM vm)
        {
            var images = await _context.OfferImages
                .Where(x => x.OurOfferId == vm.OurOfferId)
                .ToListAsync();

            await ReplaceImageAsync(images.FirstOrDefault(x => x.ImageType == "Main"), vm.MainImage);
            await ReplaceImageAsync(images.FirstOrDefault(x => x.ImageType == "Left"), vm.LeftImage);
            await ReplaceImageAsync(images.FirstOrDefault(x => x.ImageType == "Right"), vm.RightImage);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            var images = await _context.OfferImages.ToListAsync();
            foreach (var image in images)
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/offerimages", image.ImageUrl);
                if (File.Exists(path)) File.Delete(path);
            }

            _context.OfferImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveImageAsync(IFormFile file, string type, int offerId)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "offerimages");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName; 
        }



        private async Task ReplaceImageAsync(OfferImage? entity, IFormFile? newFile)
        {
            if (entity == null || newFile == null) return;

            string newFileName = Guid.NewGuid() + Path.GetExtension(newFile.FileName);
            string newPath = Path.Combine(_env.WebRootPath, "uploads/offerimages", newFileName);

            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                await newFile.CopyToAsync(stream);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "uploads/offerimages", entity.ImageUrl);
            if (File.Exists(oldPath)) File.Delete(oldPath);

            entity.ImageUrl = newFileName;
        }
    }
}
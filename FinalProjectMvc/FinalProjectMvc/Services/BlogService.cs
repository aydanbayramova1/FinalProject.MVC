using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<BlogVM>> GetAllAsync()
        {
            var blogs = await _context.Blogs.OrderByDescending(x => x.CreateDate)
                                            .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);           
    
        }

        public async Task<BlogDetailVM> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null) throw new KeyNotFoundException("Blog not found");
            return _mapper.Map<BlogDetailVM>(blog);
        }

        public async Task CreateAsync(BlogCreateVM vm)
        {
            var blog = new Blog
            {
                Title = vm.Title,
                Description = vm.Description,
                CreateDate = vm.Date
            };

            if (vm.ImgFile != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "blogs");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImgFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImgFile.CopyToAsync(stream);
                }

                blog.Img = fileName;
            }

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, BlogEditVM vm)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null) throw new KeyNotFoundException("Blog not found");

            blog.Title = vm.Title;
            blog.Description = vm.Description;

            if (vm.ImgFile != null)
            {
                string oldImagePath = Path.Combine(_env.WebRootPath, "uploads/blogs", blog.Img);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImgFile.FileName);
                string newImagePath = Path.Combine(_env.WebRootPath, "uploads/blogs", fileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await vm.ImgFile.CopyToAsync(stream);
                }

                blog.Img = fileName;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null) throw new KeyNotFoundException("Blog not found");

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BlogVM> GetAllBlogs()
        {
            return _context.Blogs
                .OrderByDescending(b => b.Id) 
                .Select(b => new BlogVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Img = b.Img,
                    Date = b.CreateDate
                })
                .ToList();
        }
    }
}

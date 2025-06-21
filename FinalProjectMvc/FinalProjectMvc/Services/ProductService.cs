using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<Product> CreateAsync(ProductCreateVM vm)
        {
            bool isDuplicate = await _context.Products
                .AnyAsync(p => p.Name.ToLower() == vm.Name.ToLower());
            if (isDuplicate)
                throw new ArgumentException("A product with the same name already exists.");

            var product = _mapper.Map<Product>(vm);

            if (vm.ImageFile != null)
            {
                product.Image = await vm.ImageFile.SaveFileAsync(_env.WebRootPath, "uploads/products");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // product.Id burada artıq var

            var categoryType = await GetCategoryTypeAsync(product.CategoryId);

            if (categoryType == "Drink" && vm.SelectedSizeIds != null && vm.SelectedSizeIds.Any())
            {
                foreach (var sizeId in vm.SelectedSizeIds)
                {
                    await _context.ProductSizes.AddAsync(new ProductSize
                    {
                        ProductId = product.Id,
                        SizeId = sizeId
                    });
                }

                await _context.SaveChangesAsync();
            }

            return product; // ✅ yeni məhsulu geri qaytar
        }


        public async Task<ProductCreateVM> GetCreateVMAsync()
        {
            var vm = new ProductCreateVM
            {
                Categories = await _context.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync(),
                Sizes = await _context.Sizes
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .ToListAsync(),
                CategoryTypesById = await _context.Categories
                    .Include(c => c.CategoryType)
                    .ToDictionaryAsync(c => c.Id, c => c.CategoryType.Name)
            };

            return vm;
        }
        public async Task<IQueryable<ProductVM>> GetAllQueryAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .ToListAsync();

            var mapped = _mapper.Map<List<ProductVM>>(products);
            return mapped.AsQueryable();
        }
        public async Task<ProductEditVM> GetEditVMAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            var vm = _mapper.Map<ProductEditVM>(product);

            vm.Categories = await _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();

            vm.Sizes = await _context.Sizes
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToListAsync();

            vm.CategoryTypesById = await _context.Categories
                .Include(c => c.CategoryType)
                .ToDictionaryAsync(c => c.Id, c => c.CategoryType.Name);

            if (product.Category?.CategoryType?.Name == "Drink")
            {
                vm.SelectedSizeIds = product.ProductSizes?.Select(ps => ps.SizeId).ToList() ?? new List<int>();
            }

            return vm;
        }
        public async Task<List<Product>> SearchAsync(string query)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.Name.Contains(query) ||
                    p.Category.Name.Contains(query) ||
                    p.Price.ToString().Contains(query))
                .ToListAsync();
        }
        public async Task EditAsync(ProductEditVM vm)
        {
            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == vm.Id);

            if (product == null) throw new KeyNotFoundException("Product not found");

            bool isDuplicate = await _context.Products
                .AnyAsync(p => p.Id != vm.Id && p.Name.ToLower() == vm.Name.ToLower());
            if (isDuplicate)
                throw new ArgumentException("A product with the same name already exists.");

            _mapper.Map(vm, product);

            if (vm.ImageFile != null)
            {
                product.Image = await vm.ImageFile.SaveFileAsync(_env.WebRootPath, "uploads/products");
            }

            _context.ProductSizes.RemoveRange(product.ProductSizes);

            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            if (categoryType == "Drink" && vm.SelectedSizeIds != null && vm.SelectedSizeIds.Any())
            {
                foreach (var sizeId in vm.SelectedSizeIds)
                {
                    _context.ProductSizes.Add(new ProductSize
                    {
                        ProductId = product.Id,
                        SizeId = sizeId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<ProductDetailVM> GetDetailAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category).ThenInclude(c => c.CategoryType)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return _mapper.Map<ProductDetailVM>(product);
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {

            var products = await _context.Products
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryType)
                .Include(p => p.ProductSizes)
                    .ThenInclude(ps => ps.Size)
                .OrderByDescending(p => p.Id) 
                .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateProductAsync(ProductCreateVM vm)
        {
            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            if (categoryType == "Drink" && (vm.SelectedSizeIds == null || vm.SelectedSizeIds.Count != 3))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateProductAsync(ProductEditVM vm)
        {
            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            if (categoryType == "Drink" && (vm.SelectedSizeIds == null || vm.SelectedSizeIds.Count != 3))
            {
                return false;
            }

            return true;
        }
        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }


        public async Task<string> GetCategoryTypeAsync(int categoryId)
        {
            try
            {
                var categoryType = await _context.Categories
                    .Include(c => c.CategoryType)
                    .Where(c => c.Id == categoryId)
                    .Select(c => c.CategoryType.Name)
                    .FirstOrDefaultAsync();

                return categoryType ?? "";
            }
            catch
            {
                return "";
            }
        }
    }


}
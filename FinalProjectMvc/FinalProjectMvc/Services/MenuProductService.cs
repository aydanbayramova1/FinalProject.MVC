using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.MenuProduct;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class MenuProductService : IMenuProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MenuProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(MenuProductCreateVM vm)
        {
            var menuProduct = _mapper.Map<MenuProduct>(vm);

            await _context.MenuProducts.AddAsync(menuProduct);
            await _context.SaveChangesAsync();

            var categoryType = await GetCategoryTypeAsync(menuProduct.CategoryId);

            if (categoryType == "Drink" && vm.SelectedSizeIds != null && vm.SelectedSizeIds.Any())
            {
                foreach (var sizeId in vm.SelectedSizeIds)
                {
                    await _context.MenuProductSizes.AddAsync(new MenuProductSize
                    {
                        MenuProductId = menuProduct.Id,
                        SizeId = sizeId
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<MenuProductCreateVM> GetCreateVMAsync()
        {
            return new MenuProductCreateVM
            {
                Categories = await _context.Categories
                    .Include(c => c.CategoryType) // CategoryType-ı include et
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync(),
                Sizes = await _context.Sizes
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .ToListAsync(),
                CategoryTypesById = await _context.Categories
                    .Include(c => c.CategoryType)
                    .ToDictionaryAsync(c => c.Id, c => c.CategoryType.Name ?? "")
            };
        }

        public async Task<MenuProductEditVM> GetEditVMAsync(int id)
        {
            var menuProduct = await _context.MenuProducts
                .Include(p => p.MenuProductSizes)
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (menuProduct == null) return null;

            var vm = _mapper.Map<MenuProductEditVM>(menuProduct);

            vm.Categories = await _context.Categories
                .Include(c => c.CategoryType) // CategoryType-ı include et
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync();

            vm.Sizes = await _context.Sizes
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToListAsync();

            vm.CategoryTypesById = await _context.Categories
                .Include(c => c.CategoryType)
                .ToDictionaryAsync(c => c.Id, c => c.CategoryType.Name ?? "");

            // Drink kategoriyası üçün size-ları set et
            if (menuProduct.Category?.CategoryType?.Name == "Drink")
            {
                vm.SelectedSizeIds = menuProduct.MenuProductSizes?.Select(ps => ps.SizeId).ToList() ?? new List<int>();
            }
            else
            {
                vm.SelectedSizeIds = new List<int>(); // Boş list ver
            }

            return vm;
        }

        public async Task EditAsync(MenuProductEditVM vm)
        {
            var menuProduct = await _context.MenuProducts
                .Include(p => p.MenuProductSizes)
                .FirstOrDefaultAsync(p => p.Id == vm.Id);

            if (menuProduct == null) throw new Exception("Menu Product not found");

            _mapper.Map(vm, menuProduct);

            // Əvvəlki size-ları sil
            _context.MenuProductSizes.RemoveRange(menuProduct.MenuProductSizes);

            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            // Yalnız Drink kategoriyası üçün size-lar əlavə et
            if (categoryType == "Drink" && vm.SelectedSizeIds != null && vm.SelectedSizeIds.Any())
            {
                foreach (var sizeId in vm.SelectedSizeIds)
                {
                    _context.MenuProductSizes.Add(new MenuProductSize
                    {
                        MenuProductId = menuProduct.Id,
                        SizeId = sizeId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<MenuProductDetailVM> GetDetailAsync(int id)
        {
            var menuProduct = await _context.MenuProducts
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryType)
                .Include(p => p.MenuProductSizes)
                    .ThenInclude(ps => ps.Size)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (menuProduct == null) return null;

            return _mapper.Map<MenuProductDetailVM>(menuProduct);
        }

        public async Task<List<MenuProductVM>> GetAllAsync()
        {
            var products = await _context.MenuProducts
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryType)
                .Include(p => p.MenuProductSizes)
                    .ThenInclude(ps => ps.Size)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return _mapper.Map<List<MenuProductVM>>(products);
        }

        public async Task DeleteAsync(int id)
        {
            var menuProduct = await _context.MenuProducts.FindAsync(id);
            if (menuProduct == null) return;

            _context.MenuProducts.Remove(menuProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateMenuProductAsync(MenuProductCreateVM vm)
        {
            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            // Drink kategoriyası üçün həmişə 3 size seçilməlidir
            if (categoryType == "Drink")
            {
                return vm.SelectedSizeIds != null && vm.SelectedSizeIds.Count == 3;
            }

            // Digər kateqoriyalar üçün size seçimi məcburi deyil
            return true;
        }

        public async Task<bool> ValidateMenuProductAsync(MenuProductEditVM vm)
        {
            var categoryType = await GetCategoryTypeAsync(vm.CategoryId);

            // Drink kategoriyası üçün həmişə 3 size seçilməlidir
            if (categoryType == "Drink")
            {
                return vm.SelectedSizeIds != null && vm.SelectedSizeIds.Count == 3;
            }

            // Digər kateqoriyalar üçün size seçimi məcburi deyil
            return true;
        }

        public async Task<string> GetCategoryTypeAsync(int categoryId)
        {
            var result = await _context.Categories
                .Include(c => c.CategoryType)
                .Where(c => c.Id == categoryId)
                .Select(c => c.CategoryType.Name)
                .FirstOrDefaultAsync();

            return result ?? "Unknown";
        }
    }
}
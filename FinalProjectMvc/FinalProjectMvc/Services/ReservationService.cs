using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.Product;
using FinalProjectMvc.ViewModels.Admin.ProductSize;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using FinalProjectMvc.ViewModels.Admin.Size;
using FinalProjectMvc.ViewModels.Admin.Table;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ReservationService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<List<ReservationVM>> GetAllAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Table)
                .Include(r => r.Order)
                .ToListAsync();

            return reservations.Select(r => new ReservationVM
            {
                Id = r.Id,
                FullName = r.FullName,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                Date = r.Date,
                Time = r.Time,
                GuestCount = r.GuestCount,
                TableNumber = r.Table.Number.ToString(),
                IsConfirmed = r.IsConfirmed,
                IsRejected = r.IsRejected,
                IsPaid = r.IsPaid
            }).ToList();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Table)
                .Include(r => r.Order)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> CreateAsync(ReservationCreateVM vm)
        {
            var table = await _context.Tables
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t =>
                    t.Capacity == vm.GuestCount &&
                    !t.Reservations.Any(r => r.Date == vm.Date && r.Time == vm.Time && !r.IsCanceled)
                );

            if (table == null) return false;

            var orderItems = vm.CartItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                SizeId = i.SizeId,
                Quantity = i.Quantity,
                UnitPrice = GetPrice(i.ProductId, i.SizeId)
            }).ToList();

            var reservation = new Reservation
            {
                FullName = vm.FullName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                Date = vm.Date,
                Time = vm.Time,
                GuestCount = vm.GuestCount,
                TableId = table.Id,
                Order = new Order
                {
                    Items = orderItems,
                    TotalAmount = orderItems.Sum(i => i.UnitPrice * i.Quantity)
                }
            };

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        private decimal GetPrice(int productId, int sizeId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            return product?.Price ?? 0; 
        }

        public async Task<List<ProductWithSizeVM>> GetMenuProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category).ThenInclude(c => c.CategoryType)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

            var grouped = products
                .GroupBy(p => p.CategoryId)
                .Select(g => g.Take(10))
                .SelectMany(p => p)
                .ToList();

            var result = grouped.Select(p => new ProductWithSizeVM
            {
                Id = p.Id,
                Name = p.Name,
                Ingredients = p.Ingredients,
                Price = p.Price,
                CategorySlug = p.Category.Name.ToLower().Replace(" ", "-"),
                CategoryName = p.Category.Name,
                CategoryTypeId = p.Category.CategoryTypeId,
                Sizes = p.Category.CategoryType.Name == "Drink"
                    ? p.ProductSizes.Select(ps => new SizeVM
                    {
                        Id = ps.SizeId,
                        Name = ps.Size.Name,
                    }).ToList()
                    : null
            }).ToList();

            return result;
        }


        public async Task<List<CategoryVM>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryVMs = categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return categoryVMs;
        }
        public async Task<List<TableVM>> GetAllTablesAsync()
        {
            return await _context.Tables
                .Select(t => new TableVM
                {
                    Id = t.Id,
                    Number = t.Number,
                    Capacity = t.Capacity,
                    Location = t.Location
                }).ToListAsync();
        }
        public async Task<bool> ApproveAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Order)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return false;

            reservation.IsConfirmed = true;
            reservation.IsRejected = false;

            await _context.SaveChangesAsync();

            string link = $"https://yoursite.com/Payment/Index?reservationId={reservation.Id}";
            await _emailService.SendAsync(reservation.Email, "Rezervasiyanız təsdiqləndi",
                $"<p>Rezervasiyanız təsdiqləndi. <a href='{link}'>Ödənişə keç</a></p>");

            return true;
        }

        public async Task<bool> RejectAsync(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null) return false;

            reservation.IsRejected = true;
            reservation.IsConfirmed = false;

            await _context.SaveChangesAsync();

            await _emailService.SendAsync(reservation.Email, "Rezervasiya rədd edildi",
                "<p>Üzr istəyirik, rezervasiyanız rədd edildi.</p>");
            return true;
        }

        public async Task CheckAndDeleteExpiredReservationsAsync()
        {
            var now = DateTime.Now;
            var expired = await _context.Reservations
                .Where(r => !r.IsConfirmed && r.Date.AddHours(-2) <= now)
                .ToListAsync();

            _context.Reservations.RemoveRange(expired);
            await _context.SaveChangesAsync();
        }

        public async Task SendReminderEmailsAsync()
        {
            var targetTime = DateTime.Now.AddHours(2);
            var upcoming = await _context.Reservations
                .Where(r => r.IsConfirmed && !r.IsPaid && r.Date.Date == targetTime.Date && r.Time.Hours == targetTime.Hour)
                .ToListAsync();

            foreach (var reservation in upcoming)
            {
                await _emailService.SendAsync(
                    reservation.Email,
                    "Reminder: Your Reservation is in 2 Hours",
                    $"Dear {reservation.FullName},<br/>Your reservation is scheduled for {reservation.Date:dd MMM yyyy} at {reservation.Time:hh\\:mm}.<br/>If you can’t attend, please cancel in advance.");
            }
        }

        public async Task<List<ProductWithSizeVM>> GetLast10ProductsPerCategoryAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category).ThenInclude(c => c.CategoryType)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

            var grouped = products
                .GroupBy(p => p.CategoryId)
                .Select(g => g.Take(10))
                .SelectMany(p => p)
                .ToList();

            var result = grouped.Select(p => new ProductWithSizeVM
            {
                Id = p.Id,
                Name = p.Name,
                Ingredients = p.Ingredients,
                Price = p.Price,
                CategorySlug = p.Category.Name.ToLower().Replace(" ", "-"),
                CategoryName = p.Category.Name,
                CategoryTypeId = p.Category.CategoryTypeId,
                Sizes = p.Category.CategoryType.Name == "Drink"
                    ? p.ProductSizes.Select((ps, index) => new SizeVM
                    {
                        Id = ps.SizeId,
                        Name = ps.Size.Name,
                        Price = p.Price + (index * 2)
                    }).ToList()
                    : null
            }).ToList();

            return result;
        }
        public async Task<List<ProductWithSizeVM>> GetLastProductsPerCategoryAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category).ThenInclude(c => c.CategoryType)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

            var grouped = products
                .GroupBy(p => p.CategoryId)
                .Select(g => g.First()) 
                .ToList();

            var result = grouped.Select(p => new ProductWithSizeVM
            {
                Id = p.Id,
                Name = p.Name,
                Ingredients = p.Ingredients,
                Price = p.Price,
                CategorySlug = p.Category.Name.ToLower().Replace(" ", "-"),
                CategoryName = p.Category.Name,
                CategoryTypeId = p.Category.CategoryTypeId,
                Sizes = p.Category.CategoryType.Name == "Drink"
                    ? p.ProductSizes.Select(ps => new SizeVM
                    {
                        Id = ps.SizeId,
                        Name = ps.Size.Name,
                    }).ToList()
                    : null
            }).ToList();

            return result;
        }

    }
}

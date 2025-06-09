using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Setting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMvc.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<bool> ExistsAsync()
        {
            return await _context.Settings.AnyAsync();
        }

        public async Task<SettingVM?> GetSettingAsync()
        {
            var settings = await _context.Settings.ToListAsync();
            if (settings == null || !settings.Any()) return null;

            return new SettingVM
            {
                Id = settings.FirstOrDefault()?.Id ?? 0,
                HeaderLogo = settings.FirstOrDefault(s => s.Key == "HeaderLogo")?.Value,
                FooterLogo = settings.FirstOrDefault(s => s.Key == "FooterLogo")?.Value,
                BackgroundImage = settings.FirstOrDefault(s => s.Key == "BackgroundImage")?.Value,
                Address = settings.FirstOrDefault(s => s.Key == "Address")?.Value,
                Email = settings.FirstOrDefault(s => s.Key == "Email")?.Value,
                Phone = settings.FirstOrDefault(s => s.Key == "Phone")?.Value,
                EverydayWorkingHours = settings.FirstOrDefault(s => s.Key == "EverydayWorkingHours")?.Value,
                KitchenCloseTime = settings.FirstOrDefault(s => s.Key == "KitchenCloseTime")?.Value,
            };
        }

        public async Task CreateAsync(SettingCreateVM model)
        {
            if (await ExistsAsync())
                throw new Exception("Settings already exist.");

            var entries = new List<Models.Setting>();

            entries.Add(new Models.Setting
            {
                Key = "HeaderLogo",
                Value = await SaveFileAsync(model.HeaderLogo)
            });
            entries.Add(new Models.Setting
            {
                Key = "FooterLogo",
                Value = await SaveFileAsync(model.FooterLogo)
            });
            entries.Add(new Models.Setting
            {
                Key = "BackgroundImage",
                Value = await SaveFileAsync(model.BackgroundImage)
            });
            entries.Add(new Models.Setting { Key = "Address", Value = model.Address });
            entries.Add(new Models.Setting { Key = "Email", Value = model.Email });
            entries.Add(new Models.Setting { Key = "Phone", Value = model.Phone });
            entries.Add(new Models.Setting { Key = "EverydayWorkingHours", Value = model.EverydayWorkingHours });
            entries.Add(new Models.Setting { Key = "KitchenCloseTime", Value = model.KitchenCloseTime });

            await _context.Settings.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

      
        public async Task DeleteAllAsync()
        {
            var allSettings = await _context.Settings.ToListAsync();
            _context.Settings.RemoveRange(allSettings);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateOrAddSettingAsync(List<Models.Setting> settings, string key, IFormFile file)
        {
            var setting = settings.FirstOrDefault(s => s.Key == key);

            if (file != null)
            {
                var newValue = await SaveFileAsync(file);
                if (setting != null)
                    setting.Value = newValue;
                else
                    settings.Add(new Models.Setting { Key = key, Value = newValue });
            }
    
        }



     

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + fileName; 
        }
        public async Task<SettingEditVM?> GetEditModelAsync()
        {
            var settings = await _context.Settings.ToListAsync();
            if (!settings.Any()) return null;

            return new SettingEditVM
            {
                Id = settings.FirstOrDefault()?.Id ?? 0,
                HeaderLogo = settings.FirstOrDefault(s => s.Key == "HeaderLogo")?.Value,
                FooterLogo = settings.FirstOrDefault(s => s.Key == "FooterLogo")?.Value,
                BackgroundImage = settings.FirstOrDefault(s => s.Key == "BackgroundImage")?.Value,
                Address = settings.FirstOrDefault(s => s.Key == "Address")?.Value,
                Email = settings.FirstOrDefault(s => s.Key == "Email")?.Value,
                Phone = settings.FirstOrDefault(s => s.Key == "Phone")?.Value,
                EverydayWorkingHours = settings.FirstOrDefault(s => s.Key == "EverydayWorkingHours")?.Value,
                KitchenCloseTime = settings.FirstOrDefault(s => s.Key == "KitchenCloseTime")?.Value,
            };
        }

        public async Task EditAsync(SettingEditVM model)
        {
            var settings = await _context.Settings.ToListAsync();

            await UpdateOrAddSettingAsync(settings, "HeaderLogo", model.HeaderLogoFile, model.HeaderLogo);
            await UpdateOrAddSettingAsync(settings, "FooterLogo", model.FooterLogoFile, model.FooterLogo);
            await UpdateOrAddSettingAsync(settings, "BackgroundImage", model.BackgroundImageFile, model.BackgroundImage);

            UpdateOrAddSetting(settings, "Address", model.Address);
            UpdateOrAddSetting(settings, "Email", model.Email);
            UpdateOrAddSetting(settings, "Phone", model.Phone);
            UpdateOrAddSetting(settings, "EverydayWorkingHours", model.EverydayWorkingHours);
            UpdateOrAddSetting(settings, "KitchenCloseTime", model.KitchenCloseTime);

            _context.Settings.UpdateRange(settings);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateOrAddSettingAsync(List<Setting> settings, string key, IFormFile file, string existingValue)
        {
            var setting = settings.FirstOrDefault(s => s.Key == key);
            if (file != null)
            {
                if (!string.IsNullOrWhiteSpace(existingValue))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, existingValue.TrimStart('/'));
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                var newPath = await SaveFileAsync(file);
                if (setting != null)
                    setting.Value = newPath;
                else
                    settings.Add(new Setting { Key = key, Value = newPath });
            }
        }

        private void UpdateOrAddSetting(List<Setting> settings, string key, string value)
        {
            var setting = settings.FirstOrDefault(s => s.Key == key);
            if (setting != null)
                setting.Value = value;
            else
                settings.Add(new Setting { Key = key, Value = value });
        }
    }
}

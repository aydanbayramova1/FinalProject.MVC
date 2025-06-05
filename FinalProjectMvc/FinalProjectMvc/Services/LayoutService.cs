﻿using FinalProjectMvc.Data;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}

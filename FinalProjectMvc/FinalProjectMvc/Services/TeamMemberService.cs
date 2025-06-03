using AutoMapper;
using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Extensions;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.TeamMember;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinalProjectMvc.Services.Implementations
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TeamMemberService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<TeamMemberVM>> GetAllAsync()
        {
            var members = await _context.TeamMembers.ToListAsync();
            return _mapper.Map<List<TeamMemberVM>>(members);
        }

        public async Task<TeamMember> GetByIdAsync(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) throw new KeyNotFoundException("Team member not found");
            return member;
        }

        public async Task CreateAsync(TeamMemberCreateVM vm)
        {
            if (!Regex.IsMatch(vm.FullName, @"^[a-zA-Z\s]+$"))
                throw new BadHttpRequestException("Name can only contain letters");

            if (!Regex.IsMatch(vm.Position, @"^[a-zA-Z\s]+$"))
                throw new BadHttpRequestException("Position can only contain letters");

            string fileName = await vm.Photo.SaveFileAsync(_env.WebRootPath, "uploads/team");

            var member = _mapper.Map<TeamMember>(vm);
            member.Img = fileName;

            await _context.TeamMembers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, TeamMemberEditVM vm)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) throw new KeyNotFoundException("Team member not found");

            if (!Regex.IsMatch(vm.FullName, @"^[a-zA-Z\s]+$"))
                throw new BadHttpRequestException("Name can only contain letters");

            if (!Regex.IsMatch(vm.Position, @"^[a-zA-Z\s]+$"))
                throw new BadHttpRequestException("Position can only contain letters");

            if (vm.Photo != null)
            {
                string fileName = await vm.Photo.SaveFileAsync(_env.WebRootPath, "uploads/team");
                member.Img.DeleteFile(_env.WebRootPath, "assets/images/common");
                member.Img = fileName;
            }

            _mapper.Map(vm, member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) throw new KeyNotFoundException("Team member not found");

            member.Img.DeleteFile(_env.WebRootPath, "uploads/team");
            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();
        }
    }
}

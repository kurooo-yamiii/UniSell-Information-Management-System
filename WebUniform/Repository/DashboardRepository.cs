using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.Data;
using Microsoft.EntityFrameworkCore;

namespace WebUniform.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly Database _context;
        public DashboardRepository(Database context)
        {
            _context = context;
        }

        public async Task<List<Slack>> GetSlacksByUserID(int id)
        {
            return await _context.Slacks.Include(i => i.Address).Where(c => c.UserId == id).ToListAsync();
        }

        public async Task<List<Uniform>> GetUniformsByUserID(int id)
        {
            return await _context.Uniforms.Include(i => i.Address).Where(c => c.UserId == id).ToListAsync();
        }

        public async Task<List<Uniform>> GetRecentUniform()
        {
            return await _context.Uniforms.Include(i => i.Address)
                .OrderByDescending(u => u.Id)
                .Take(3)
                .ToListAsync();
        }

        public async Task<List<Slack>> GetRecentSlack()
        {
            return await _context.Slacks.Include(i => i.Address)
                .OrderByDescending(u => u.Id)
                .Take(3)
                .ToListAsync();
        }
    }
}

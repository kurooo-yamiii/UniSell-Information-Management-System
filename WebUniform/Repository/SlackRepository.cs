
using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.Data;
using Microsoft.EntityFrameworkCore;

namespace WebUniform.Repository
{
    public class SlackRepository : ISlackRepository
    {
        private readonly Database _context;
        public SlackRepository(Database context)
        {
            _context = context;
        }
        public bool Add(Slack slack)
        {
            _context.Add(slack);
            return Save();
        }

        public bool Delete(Slack slack)
        {
            _context.Remove(slack);
            return Save();
        }

        public async Task<IEnumerable<Slack>> GetAll()
        {
            return await _context.Slacks.Include(s => s.Address).ToListAsync();
        }

        public async Task<Slack> GetByIdAsync(int id)
        {
            await UpdateRandomSlackIdsAsync(id);
            return await _context.Slacks.Include(i => i.Address).Include(u => u.User).Include(s => s.RelatedItems).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Slack> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Slacks.Include(i => i.Address).Include(u => u.User).Include(s => s.RelatedItems).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Slack>> GetSlacksByCity(string city)
        {
            return await _context.Slacks.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Slack slack)
        {
            _context.Update(slack);
            return Save();
        }

        public async Task UpdateRandomSlackIdsAsync(int id)
        {
         
            var randomSlacks = await GetRandomSlacksAsync(4);

          
            foreach (var slack in randomSlacks)
            {
                slack.SlackId = id; 
            }

           
            await _context.SaveChangesAsync();
        }

        private async Task<List<Slack>> GetRandomSlacksAsync(int count)
        {
            
            var randomSlacks = await _context.Slacks
                .OrderBy(x => Guid.NewGuid()) 
                .Take(count) 
                .ToListAsync();

            return randomSlacks;
        }

        public async Task<IEnumerable<Slack>> SearchAsync(string searchedTerm)
        {
            return await _context.Slacks.Include(i => i.Address).Include(u => u.User).Where(s => s.Waist.Contains(searchedTerm) 
                            || s.Length.Contains(searchedTerm) 
                            || s.Status.Contains(searchedTerm) 
                            || s.User.Department.Contains(searchedTerm) 
                            || s.User.Name.Contains(searchedTerm)
                            || s.Address.City.Contains(searchedTerm)
                            || s.Address.State.Contains(searchedTerm)
                            || s.Address.Street.Contains(searchedTerm))
                                .ToListAsync();
        }

    }
}

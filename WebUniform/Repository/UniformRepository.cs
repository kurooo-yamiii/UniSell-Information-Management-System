using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.Data;
using Microsoft.EntityFrameworkCore;

namespace WebUniform.Repository
{
    public class UniformRepository : IUniformRepository
    {
        private readonly Database _context;
        public UniformRepository(Database context)
        {
            _context = context;
        }
        public bool Add(Uniform uniform)
        {
            _context.Add(uniform);
            return Save();
        }

        public bool Delete(Uniform uniform)
        {
            _context.Remove(uniform);
            return Save();
        }

        public async Task<IEnumerable<Uniform>> GetAll()
        {
            return await _context.Uniforms.Include(s => s.Address).ToListAsync();
        }

        public async Task<Uniform> GetByIdAsync(int id)
        {
            await UpdateRandomUniformIdsAsync(id);
            return await _context.Uniforms.Include(i => i.Address).Include(u => u.User).Include(s => s.RelatedItems).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Uniform> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Uniforms.Include(i => i.Address).Include(u => u.User).Include(s => s.RelatedItems).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Uniform>> GetUniformByCity(string city)
        {
            return await _context.Uniforms.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Uniform uniform)
        {
            _context.Update(uniform);
            return Save();
        }

        public async Task UpdateRandomUniformIdsAsync(int id)
        {
            var randomUniform = await GetRandomUniformAsync(4);


            foreach (var slack in randomUniform)
            {
                slack.UniformId = id;
            }


            await _context.SaveChangesAsync();
        }
        private async Task<List<Uniform>> GetRandomUniformAsync(int count)
        {

            var randomUniform = await _context.Uniforms
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToListAsync();

            return randomUniform;
        }

        public async Task<IEnumerable<Uniform>> SearchAsync(string searchedTerm)
        {
            return await _context.Uniforms.Where(s => s.Sleeve.Contains(searchedTerm)
                            || s.Length.Contains(searchedTerm)
                            || s.Shoulder.Contains(searchedTerm)
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

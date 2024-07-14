using WebUniform.Models;

namespace WebUniform.Interface
{
    public interface IUniformRepository
    {
        Task<IEnumerable<Uniform>> GetAll();

        Task<Uniform> GetByIdAsync(int id);
       
        Task<Uniform> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Uniform>> GetUniformByCity(string city);

        // Then eto CRUD inside the database
        bool Add(Uniform uniform);

        bool Update(Uniform uniform);

        bool Delete(Uniform uniform);
        bool Save();

        Task UpdateRandomUniformIdsAsync(int id);
        Task<IEnumerable<Uniform>> SearchAsync(string searchedTerm);
    }
}

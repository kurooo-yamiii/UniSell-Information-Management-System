using System.Threading.Tasks;
using WebUniform.Models;

namespace WebUniform.Interface
{
    public interface ISlackRepository
    {
        Task<IEnumerable<Slack>> GetAll();
   
        Task<Slack> GetByIdAsync(int id);
     
        Task<Slack> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Slack>> GetSlacksByCity(string city);

        bool Add(Slack slack);

        bool Update(Slack slack);

        bool Delete(Slack slack);
        bool Save();

        Task UpdateRandomSlackIdsAsync(int id);
        Task<IEnumerable<Slack>> SearchAsync(string searchedTerm);
    }
}

using WebUniform.Models;

namespace WebUniform.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Slack>> GetSlacksByUserID(int id);
        Task<List<Uniform>> GetUniformsByUserID(int id);
        Task<List<Slack>> GetRecentSlack();
        Task<List<Uniform>> GetRecentUniform();
    }
}

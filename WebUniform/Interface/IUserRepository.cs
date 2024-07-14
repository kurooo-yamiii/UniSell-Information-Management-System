using WebUniform.Models;

namespace WebUniform.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<bool> VerifyPassword(string username, string password);
        bool Add(User user);

        bool Update(User user);

        bool Delete(User user);

        bool Save();
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebUniform.Data;
using WebUniform.Interface;
using WebUniform.Models;


namespace WebUniform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Database _context;
        public UserRepository(Database context)
        {
            _context = context;
        }
        public bool Add(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Username == email);
        }


        public async Task<bool> VerifyPassword(string username, string password)
        {
          
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (dbUser == null)
            {
                return false;
            }

            return dbUser.Password == password;

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}


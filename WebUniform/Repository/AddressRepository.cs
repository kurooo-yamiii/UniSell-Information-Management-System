using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.Data;

namespace WebUniform.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly Database _context;
        public AddressRepository(Database context)
        {
            _context = context;
        }
        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Address address)
        {
            _context.Update(address);
        }
    }
}

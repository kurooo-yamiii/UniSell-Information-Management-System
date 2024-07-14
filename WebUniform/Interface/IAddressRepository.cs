using WebUniform.Models;

namespace WebUniform.Interface
{
    public interface IAddressRepository
    {
        Task<Address> GetByIdAsync(int id);
        void Update(Address address);
        Task SaveAsync();
    }
}

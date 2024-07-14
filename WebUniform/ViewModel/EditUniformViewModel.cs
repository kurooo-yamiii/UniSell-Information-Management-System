using WebUniform.Models;

namespace WebUniform.ViewModel
{
    public class EditUniformViewModel
    {
        public int Id { get; set; }
        public string Shoulder { get; set; }
        public string Sleeve { get; set; }
        public string Length { get; set; }
        public int AddressId { get; set; }
        public string? URL { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
    }
}

using WebUniform.Models;

namespace WebUniform.ViewModel
{
    public class EditSlackViewModel
    {
        public int Id { get; set; }
        public string Waist { get; set; }
        public string Length { get; set; }
        public int AddressId { get; set; }
        public string? URL { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
    }
}

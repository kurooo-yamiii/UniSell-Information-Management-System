using WebUniform.Models;

namespace WebUniform.ViewModel
{
    public class UniformCreateModel
    {
        public int Id { get; set; }
        public string Shoulder { get; set; }
        public string Sleeve { get; set; }
        public string Length { get; set; }
        public IFormFile Image { get; set; }
        public string? Status { get; set; }
        public Address Address { get; set; }
        public int? AppUserId { get; set; }
    }
}

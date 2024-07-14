
using WebUniform.Models;

namespace WebUniform.ViewModel

{
    public class SlackCreateModel
    {

        public int Id { get; set; }
        public string Waist { get; set; }
        public string Length { get; set; }
        public IFormFile Image { get; set; }
        public string? Status { get; set; }
        public Address Address { get; set; }
        public int? AppUserId {get; set;} 
    }
}

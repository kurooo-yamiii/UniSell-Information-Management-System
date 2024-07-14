using System.ComponentModel.DataAnnotations;

namespace WebUniform.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string  Name { get; set; }
        public string Contact { get; set; }
        public string Department { get; set; }
    }
}

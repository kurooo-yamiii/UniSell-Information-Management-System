using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniform.Models
{
    public class Uniform
    {
        [Key]
        public int Id { get; set; }
        public string Shoulder { get; set; }
        public string Sleeve { get; set; }
        public string Length { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public List<Uniform> RelatedItems { get; set; } 

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? UniformId { get; internal set; }
    }
}

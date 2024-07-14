using System.ComponentModel.DataAnnotations;

namespace WebUniform.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email ADdress")]
        [Required(ErrorMessage = "Email Address is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Passowrd is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}

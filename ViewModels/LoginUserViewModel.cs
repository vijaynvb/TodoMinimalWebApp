using System.ComponentModel.DataAnnotations;

namespace TodoMinimalWebApp.ViewModels
{
    public class LoginUserViewModel
    {
        // view for login
       
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool  RememberMe { get; set; }
    }
}

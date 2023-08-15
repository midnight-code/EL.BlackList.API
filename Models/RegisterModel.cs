using System.ComponentModel.DataAnnotations;

namespace EL.BlackList.API.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        [Required(ErrorMessage = "Emai is required")]
        public string Email { get; set; } = string.Empty;
    }
}

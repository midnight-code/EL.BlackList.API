using System.ComponentModel.DataAnnotations;

namespace EL.BlackList.API.Models
{
    public class ChangePassowrdModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm new password is required")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}

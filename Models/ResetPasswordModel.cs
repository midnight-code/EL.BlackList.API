namespace EL.BlackList.API.Models
{
    public class ResetPasswordModel : ResetPasswordAdminModel
    {
        public string Token { get; set; } = string.Empty;
    }
}


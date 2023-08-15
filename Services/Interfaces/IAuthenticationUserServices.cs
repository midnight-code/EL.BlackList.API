using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IAuthenticationUserServices
    {
        Task<IBaseResponse<TokenModel>> GetLoginAsync(LoginModel login);
        Task<IBaseResponse<string>> GetRegisterAdminAsync(RegisterModel model);
        Task<IBaseResponse<string>> GetRegisterUserAsync(RegisterModel model);
        Task<IBaseResponse<string>> GetChangePasswordAsync(ChangePassowrdModel model);
        Task<IBaseResponse<string>> GetResetPasswordAdminAsync(ResetPasswordAdminModel model);
        Task<IBaseResponse<string>> GetResetPasswordTokenAsync(ResetPasswordTokenModel model);
        Task<IBaseResponse<string>> GetResetPasswordUserAsync(ResetPasswordModel model);
    }
}
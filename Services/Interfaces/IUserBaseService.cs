using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IUserBaseService
    {
        Task<IBaseResponse<UserBase>> GetUserBaseByUserIdAsync(string userId);
        Task<IBaseResponse<string>> SaveUserBaseAsync(UserBase userBase);
        Task<IBaseResponse<bool>> DeleteUserBase(string userId);
    }
}

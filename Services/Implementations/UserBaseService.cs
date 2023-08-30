using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Implementations
{
    public class UserBaseService : IUserBaseService
    {
        private readonly IUserBaseRepositore _userBaseRepositore;

        public UserBaseService(IUserBaseRepositore userBaseRepositore)
        {
            _userBaseRepositore = userBaseRepositore;
        }

        public async Task<IBaseResponse<bool>> DeleteUserBase(string userId)
        {
            if (userId == string.Empty)
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Description = $"Занчение не может быть пустым",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data=false
                });
            var result = await _userBaseRepositore.Delete(userId);
            if(result)
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Description = $"Удаление выполненно успешно!",
                    StatusCode = Enum.StatusCode.OK,
                    Data=true
                });
            else
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Description = $"Пользователь не найден!",
                    StatusCode = Enum.StatusCode.Status404NotFound,
                    Data=false
                });
        }

        public async Task<IBaseResponse<UserBase>> GetUserBaseByUserIdAsync(string userId)
        {
            if (userId == string.Empty)
                return await Task.Run(() => new BaseResponse<UserBase>()
                {
                    Description = $"Занчение не может быть пустым",
                    StatusCode = Enum.StatusCode.Status404NotFound
                });

            var userbase = await _userBaseRepositore.GetUserBaseByUserIdAsync(userId);
            if (userbase == null)
                return await Task.Run(() => new BaseResponse<UserBase>()
                {
                    Description = $"Пользователь не найден!",
                    StatusCode = Enum.StatusCode.Status404NotFound
                });
            return await Task.Run(() => new BaseResponse<UserBase>()
            {
                Description = $"",
                StatusCode = Enum.StatusCode.OK,
                Data = userbase
            });
        }

        public async Task<IBaseResponse<string>> SaveUserBaseAsync(UserBase userBase)
        {
            if (userBase == null)
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Занчение не может быть пустым",
                    StatusCode = Enum.StatusCode.Status404NotFound
                });
            var userbase = await _userBaseRepositore.SaveUserBase(userBase);
            if (userbase == string.Empty)
            {
                return await Task.Run(() => new BaseResponse<string>()
                {
                    Description = $"Запись не возможно выполнить!",
                    StatusCode = Enum.StatusCode.Status404NotFound
                });
            }
            return await Task.Run(() => new BaseResponse<string>()
            {
                Description = $"Запись выполненна успешно!",
                StatusCode = Enum.StatusCode.OK,
                Data = userbase
            });
        }
    }
}

using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface IUserBaseRepositore : IBaseRepositore<UserBase>
    {
        public Task<bool> Delete(string id);
        public Task<UserBase> GetUserBaseByUserIdAsync(string userId);
        public Task<string> SaveUserBase(UserBase intite);
    }
}

using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Repositore.Repositore
{
    public class UserBaseRepositore : IUserBaseRepositore
    {
        private readonly DataContext _dataContext;
        public UserBaseRepositore(DataContext dataContext) => _dataContext = dataContext;
        public async Task<bool> Delete(string id)
        {
            if (id == string.Empty)
                throw new NotImplementedException();
            var usebase = await _dataContext.UserBase.FirstOrDefaultAsync(i => i.UserID == id);
            if(usebase is not null)
            {
                _dataContext.UserBase.Remove(usebase);
                await _dataContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);

        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserBase?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserBase> GetUserBaseByUserIdAsync(string userId)
        {
            if(userId==string.Empty) throw new NotImplementedException();

            var userbase = await _dataContext.UserBase.FirstOrDefaultAsync(_ => _.UserID == userId);
            if(userbase is not null)
                return await Task.Run(() => userbase);
            else
                throw new NotImplementedException();
        }

        public Task<int> Save(UserBase intite)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveUserBase(UserBase intite)
        {
            if (intite == null)
                throw new NotImplementedException();

            if (intite.Id >0 )
                _dataContext.UserBase.Update(intite);
            else
                await _dataContext.UserBase.AddAsync(intite);
            await _dataContext.SaveChangesAsync();

            return await Task.Run(() => intite.UserID);
        }

        public Task<IEnumerable<UserBase>> Select()
        {
            throw new NotImplementedException();
        }
    }
}

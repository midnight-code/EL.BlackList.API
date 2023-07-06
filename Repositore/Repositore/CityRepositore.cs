using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Repositore.Repositore
{
    public class CityRepositore : ICityRepositore
    {
        private readonly DataContext _dataContext;
        public CityRepositore(DataContext dataContext) =>_dataContext = dataContext;
        public async Task<bool> Delete(int id)
        {
            if (id > 0)
            {
                var city = await _dataContext.Citys.FirstOrDefaultAsync(d => d.CityId == id);
                if(city is not null)
                {
                    _dataContext.Citys.Remove(city);
                    await _dataContext.SaveChangesAsync();
                    return await Task.Run(() => true);
                }
                
            }

            return await Task.Run(() => false);
        }

        public async Task<City?> GetById(int id)
        {
            if (id <= 0)
                return null;
            var result = await _dataContext.Citys.FirstOrDefaultAsync(c => c.CityId == id);
            if(result is not null)
                return await Task.Run(() => result);
            return null;
        }

        public async Task<City?> GetCityByName(string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                var result= await _dataContext.Citys.FirstOrDefaultAsync(c=>c.Name == cityName);
                if (result is not null)
                    return await Task.Run(() => result);
            }
            return null;
        }

        public async Task<int> Save(City intite)
        {
            if(intite is not null)
            {
                if (intite.CityId > 0)
                    _dataContext.Citys.Update(intite);
                else
                    await _dataContext.Citys.AddAsync(intite);
                await _dataContext.SaveChangesAsync();

                return await Task.Run(() => intite.CityId);
            }
            return 0;
        }

        public async Task<IEnumerable<City>> Select()
        {
            return await _dataContext.Citys.ToListAsync();
        }
    }
}

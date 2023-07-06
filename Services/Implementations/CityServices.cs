using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Implementations
{
    public class CityServices : ICityServices
    {
        private readonly ICityRepositore _cityRepositore;
        public CityServices(ICityRepositore cityRepositore) => _cityRepositore = cityRepositore;

        public async Task<IBaseResponse<bool>> DeleteCityBAsync(int id)
        {
            if (id <= 0)
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Description = $"Not Found"
                });

            var result = await _cityRepositore.Delete(id);
            return await Task.Run(() => new BaseResponse<bool>()
            {
                Description = $"",
                StatusCode = Enum.StatusCode.OK,
                Data = result
            });
        }

        public async Task<IBaseResponse<IEnumerable<City>>> GetCityAsync()
        {
            return await Task.Run(async () => new BaseResponse<IEnumerable<City>>()
            {
                Description = $"",
                StatusCode = Enum.StatusCode.OK,
                Data = await _cityRepositore.Select()
            });
        }

        public async Task<IBaseResponse<City>> GetCityByIdAsync(int id)
        {
            if (id > 0)
            {
                var result = await _cityRepositore.GetById(id);
                if (result is not null)
                    return await Task.Run(() => new BaseResponse<City>()
                    {
                        Description = $"",
                        StatusCode = Enum.StatusCode.OK,
                        Data = result
                    });
            }

            return await Task.Run(() => new BaseResponse<City>()
            {
                Description = $"Not Found"
            });
        }

        public async Task<IBaseResponse<City>> GetCityByNameAsync(string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                var result = await _cityRepositore.GetCityByName(cityName);
                if (result is not null)
                    return await Task.Run(() => new BaseResponse<City>()
                    {
                        Description = $"",
                        StatusCode = Enum.StatusCode.OK,
                        Data = result
                    });
            }
            return await Task.Run(() => new BaseResponse<City>()
            {
                 Description = $"Not Found"
            });

            
        }

        public async Task<IBaseResponse<int>> SaveCityAsync(City city)
        {
            if(city is not null)
            { 
                var result= await _cityRepositore.Save(city); 
                if(result>0)
                    return await Task.Run(() => new BaseResponse<int>()
                    {
                        Description = $"",
                        StatusCode = Enum.StatusCode.OK,
                        Data = result
                    });
            }
            return await Task.Run(() => new BaseResponse<int>()
            {
                Description = $"Not Found"
            });
        }
    }
}

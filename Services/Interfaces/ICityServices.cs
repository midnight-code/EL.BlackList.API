using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface ICityServices 
    {
        Task<IBaseResponse<IEnumerable<City>>> GetCityAsync();
        Task<IBaseResponse<City>> GetCityByNameAsync(string cityName);
        Task<IBaseResponse<City>> GetCityByIdAsync(int id);
        Task<IBaseResponse<int>> SaveCityAsync(City city);
        Task<IBaseResponse<bool>> DeleteCityBAsync(int id);
    }
}

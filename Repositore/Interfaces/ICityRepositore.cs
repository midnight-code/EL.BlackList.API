using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface ICityRepositore : IBaseRepositore<City>
    {
        Task<City?> GetCityByName (string cityName);
    }
}

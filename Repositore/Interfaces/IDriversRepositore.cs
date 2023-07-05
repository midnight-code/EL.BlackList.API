using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface IDriversRepositore : IBaseRepositore<Drivers>
    {
        Task<IEnumerable<Drivers>?> GetDriverByDateAsunc(DateTime? dateTime);
        Task<IEnumerable<Drivers>?> GetDriverByName(string firstName, string? lastName, string? secondName, DateTime? dateTime);
    }
}

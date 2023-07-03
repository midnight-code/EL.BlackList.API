using EL.BlackList.API.Models;

namespace EL.BlackList.API.Services.Repositore
{
    public interface IDriversRepositore
    {
        Task<bool> DeleteDriver(int id);
        IEnumerable<Drivers>? GetDriverByDate(DateTime? dateTime);
        IEnumerable<Drivers>? GetDriverByName(string firstName, string? lastName, string? secondName, DateTime? dateTime);
        Drivers? GetDriversId(int id);
        int SaveDriver(Drivers drivers);
    }
}
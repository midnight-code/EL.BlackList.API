using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IDriversServices
    {
        Task<IBaseResponse<IEnumerable<Drivers>>> GetDriversByNameAsync(string firstName, string? lastName, string? secondName, DateTime? dateTime); 
        Task<IBaseResponse<IEnumerable<Drivers>>> GetDriversByDateAsync(DateTime? date);
        Task<IBaseResponse<Drivers>> GetDrivreByIdAsync(int id);
        Task<IBaseResponse<int>> SaveDriverAsync(Drivers driver);
        Task<IBaseResponse<bool>> DeleteDriverBAsync(int id);
    }
}

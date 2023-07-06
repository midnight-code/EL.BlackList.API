using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface ITaxiPoolServices
    {
        Task<IBaseResponse<TaxiPools>> GetTaxiPoolByIdAsync(int id);
        Task<IBaseResponse<int>> SaveTaxiPoolAsync(TaxiPools driver);
        Task<IBaseResponse<bool>> DeleteTaxiPoolAsync(int id);
    }
}

using EL.BlackList.API.Models;

namespace EL.BlackList.API.Services.Repositore
{
    public interface ITaxiPoolRepositore
    {
        TaxiPools? GetTaxiPoolById(int id);
        int SaveTaxiPool(TaxiPools taxiPools);
        Task<bool> DeleteTaxiPool(int id);
    }
}
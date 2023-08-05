using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface IDocumentRepositore : IBaseRepositore<Documents>
    {
        Task<Documents?> GetDocumentByDriverIDAsync(int driverID, string imgType);
        Task<IEnumerable<Documents>?> GetListDocumentByDriverIDAsync(int driverID, string imgType);
    }
}

using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IDocumentsService
    {
        Task<IBaseResponse<Documents>> GetDocumentByIdAsync(int id);
        Task<IBaseResponse<Documents>> GetDocumentByDriverIdAsync(int id, string imgType);
        Task<IBaseResponse<IEnumerable<Documents>>> GetListDocumentByDriverIdAsync(int id, string imgType);
        Task<IBaseResponse<int>> SaveDocumentAsync(IFormFile file, HttpRequest httpReqest, int driverid, string imgType);
        Task<IBaseResponse<bool>> DeletedocumentAsync(int id);
    }
}

using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IDocumentsService
    {
        Task<IBaseResponse<Documents>> GetDocumentByIdAsync(int id);
        Task<IBaseResponse<int>> SaveDocumentAsync(IFormFile file, HttpRequest httpReqest);
        Task<IBaseResponse<bool>> DeletedocumentAsync(int id);
    }
}

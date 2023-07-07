using EL.BlackList.API.Enum;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Implementations
{
    public class DocumentsServices : IDocumentsService
    {
        private readonly IDocumentRepositore _documentRepositore;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentsServices(IDocumentRepositore documentRepositore, IWebHostEnvironment webHostEnvironment)
        {
            _documentRepositore = documentRepositore;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IBaseResponse<bool>> DeletedocumentAsync(int id)
        {
            if (id > 0)
            {
                await _documentRepositore.Delete(id);
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK
                });
            }
            return await Task.Run(() => new BaseResponse<bool>()
            {
                Description = $"Not Found"
            });
        }

        public async Task<IBaseResponse<Documents>> GetDocumentByIdAsync(int id)
        {
            if (id > 0)
            {
                var result = await _documentRepositore.GetById(id);
                return await Task.Run(() => new BaseResponse<Documents>()
                {
                    Data=result,
                    StatusCode = StatusCode.OK
                });
            }
            return await Task.Run(() => new BaseResponse<Documents>()
            {
                Description = $"Not Found"
            });
        }

        public async Task<IBaseResponse<int>> SaveDocumentAsync(IFormFile file, HttpRequest httpReqest)
        {
            if(file is not null)
            {
                int idDocument = 0;
                string da = Guid.NewGuid().ToString("N");
                string fileName = $"{da}.{file.FileName.Split('.')[1]}";
                //var httpReqest = httpContext.Request;
                var rootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploades", "Documents");
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);
                var path = Path.Combine(rootPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {

                    var document = new Documents()
                    {
                        FileName = fileName,
                        ContentType = file.ContentType,
                        FileSize = file.Length
                    };
                    await file.CopyToAsync(stream);
                    var path2 = Path.Combine(rootPath, $"mini_{file.FileName}");
                    //MiniImage(stream, path2);
                    idDocument = await _documentRepositore.Save(document);
                }
                return await Task.Run(() => new BaseResponse<int>()
                {
                    Data = idDocument,
                    StatusCode = StatusCode.OK
                });
            }
           
            return await Task.Run(() => new BaseResponse<int>()
            {
                Description = $"Not Found"
            });
        }

        private void MiniImage(FileStream file, string rootPath)
        {
           
        }
    }
}

using EL.BlackList.API.Enum;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;
using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;
using System.Reflection.Metadata;

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

        public async Task<IBaseResponse<Documents>> GetDocumentByDriverIdAsync(int id, string imgType)
        {
            if (id > 0)
            {
                var result = await _documentRepositore.GetDocumentByDriverIDAsync(id, imgType);
                return await Task.Run(() => new BaseResponse<Documents>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                });
            }
            return await Task.Run(() => new BaseResponse<Documents>()
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

        public async Task<IBaseResponse<IEnumerable<Documents>>> GetListDocumentByDriverIdAsync(int id, string imgType)
        {
            if (id > 0)
            {
                var result = await _documentRepositore.GetListDocumentByDriverIDAsync(id, imgType);
                return await Task.Run(() => new BaseResponse<IEnumerable<Documents>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                });
            }
            return await Task.Run(() => new BaseResponse<IEnumerable<Documents>>()
            {
                Description = $"Not Found"
            });
        }

        public async Task<IBaseResponse<int>> SaveDocumentAsync(IFormFile file, HttpRequest httpReqest, int driverid, string imgtypes)
        {
            if(file is not null)
            {
                int idDocument = 0;
                string decoder = file.FileName.Split('.')[1];

                Guid guid = Guid.NewGuid();

                string fileName = $"bl_{guid.ToString().Substring(0, 8)}_{guid.ToString().Substring(24, 12)}.{decoder}";

                var rootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploades", "Documents");
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);
                var path = Path.Combine(rootPath, fileName);
                var path_mini = Path.Combine(rootPath, $"mini_{fileName}");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    var document = new Documents()
                    {
                        FileName = fileName,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        DriverID = driverid,
                        ImgType = imgtypes
                    };
                    await file.CopyToAsync(stream);
                    idDocument = await _documentRepositore.Save(document);
                }
                await UploadAndResizeImage(path, path_mini, 400, 400, decoder);
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
        public async Task UploadAndResizeImage(string fileStream, string filename, int newWidth, int newHeight, string decoder)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

                int height = image.Height / 2;
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));
                if (decoder == "png")
                    await image.SaveAsPngAsync(filename, new PngEncoder());
                else
                    await image.SaveAsJpegAsync(filename, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}

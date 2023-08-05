using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DocumentController(IDocumentsService documentsService, IWebHostEnvironment webHostEnvironment)
        {
            _documentsService = documentsService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Document/GetDocumentByDriverid/{driverid}/{imgtypes}", Name = "GetListDocumentsByIdDriversTypes")]
        public async Task<ActionResult<IEnumerable<Documents>>> GetListDocumentsByIdDriversTypes(int driverid, string imgtypes)
        {
            var result = await _documentsService.GetListDocumentByDriverIdAsync(driverid, imgtypes);
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }


        [HttpPost("/Document/DownloadDocumentByDriverid/{driverid}/{imgtype}", Name = "DownloadDocumentbyDriverID")]
        public async Task<ActionResult> DownloadDocumentbyDriverID(int driverid, string imgtype)
        {
            var provider = new FileExtensionContentTypeProvider();
            var document = await _documentsService.GetDocumentByDriverIdAsync(driverid, imgtype);
            if (document.Data == null)
            {
                document = await _documentsService.GetDocumentByIdAsync(1);
            }
            var rootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploades", "Documents", $"{document.Data.FileName}");
            string contentType;
            bool i = provider.TryGetContentType(rootPath, out contentType);
            if (!provider.TryGetContentType(rootPath, out contentType))
            {
                contentType = "application/octet-stream";
            }
            byte[] filebyte;
            if (System.IO.File.Exists(rootPath))
            {
                filebyte = System.IO.File.ReadAllBytes(rootPath);
            }
            else
                return NotFound();
            return File(filebyte, contentType, document.Data.FileName);
        }

        [HttpPost("/Document/DownloadDocument/{id}", Name ="DownloadDocument")]
        public async Task<ActionResult> DownloadDocument(int id)
        {
            var provider = new FileExtensionContentTypeProvider();
            var document = await _documentsService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                document = await _documentsService.GetDocumentByIdAsync(1);
            }
            var rootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploades", "Documents", $"{document.Data.FileName}");
            string contentType;
            bool i = provider.TryGetContentType(rootPath, out contentType);
            if (!provider.TryGetContentType(rootPath, out contentType))
            {
                contentType = "application/octet-stream";
            }
            byte[] filebyte;
            if (System.IO.File.Exists(rootPath))
            {
                filebyte = System.IO.File.ReadAllBytes(rootPath);
            }
            else
                return NotFound();
            return File(filebyte, contentType, document.Data.FileName);
        }

        [HttpPost("/Document/UploadeDocument", Name = "UploadeDocument")]
        public async Task<ActionResult> UploadeDocument(IFormFile file, int driverid, string imgtypes)
        {
            var httpReqest = HttpContext.Request;
            var idDocument = await _documentsService.SaveDocumentAsync(file, httpReqest, driverid, imgtypes);
            
            return Ok(idDocument);
        }

       
    }
}

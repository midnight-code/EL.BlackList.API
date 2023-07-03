using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Repositore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiPoolController : ControllerBase
    {
        private readonly ITaxiPoolRepositore _taxiPoolRepositore;
        public TaxiPoolController(ITaxiPoolRepositore taxiPoolRepositore) => _taxiPoolRepositore = taxiPoolRepositore;

        [HttpGet("/TaxiPoolByID/{id}", Name = "GetTaxiPoolByID")]
        public ActionResult<TaxiPools> GetTaxiPoolByID(int id)
        {
            var result = _taxiPoolRepositore.GetTaxiPoolById(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}

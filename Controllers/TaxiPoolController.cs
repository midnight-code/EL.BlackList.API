using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Repositore;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiPoolController : ControllerBase
    {
        private readonly ITaxiPoolServices _taxiPoolServices;
        public TaxiPoolController(ITaxiPoolServices taxiPoolServices) => _taxiPoolServices = taxiPoolServices;


        [HttpGet("/TaxiPool/TaxiPoolByID/{id}", Name = "GetTaxiPoolByID")]
        public async Task<ActionResult<TaxiPools>> GetTaxiPoolByID(int id)
        {
            var result = await _taxiPoolServices.GetTaxiPoolByIdAsync(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }



        [HttpPost("/TaxiPool/SaveTaxiPool/{taxipools}", Name = "SaveTaxiPool")]
        public async Task<ActionResult<int>> SaveTaxiPool(TaxiPools taxipools)
        {
            if (taxipools is not null)
            {
                return Ok(await _taxiPoolServices.SaveTaxiPoolAsync(taxipools));
            }
            else
                return 0;
        }

        [HttpDelete("/TaxiPool/DelTaxiPool/{id}", Name = "DeleteTaxiPoolID")]
        public async Task<ActionResult<bool>> DeleteTaxiPoolID(int id)
        {
            if (id > 0)
            {
                var result = await _taxiPoolServices.DeleteTaxiPoolAsync(id);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}

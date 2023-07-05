using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Repositore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiPoolController : ControllerBase
    {
        //private readonly ITaxiPoolRepositore _taxiPoolRepositore;
        //public TaxiPoolController(ITaxiPoolRepositore taxiPoolRepositore) => _taxiPoolRepositore = taxiPoolRepositore;


        //[HttpGet("/TaxiPoolByID/{id}", Name = "GetTaxiPoolByID")]
        //public ActionResult<TaxiPools> GetTaxiPoolByID(int id)
        //{
        //    var result = _taxiPoolRepositore.GetTaxiPoolById(id);
        //    if (result is not null)
        //        return Ok(result);
        //    else
        //        return NotFound();
        //}



        //[HttpPost("/savetaxipool/{taxipools}", Name = "SaveTaxiPool")]
        //public ActionResult<int> SaveTaxiPool(TaxiPools taxipools)
        //{
        //    if (taxipools is not null)
        //    {
        //        return _taxiPoolRepositore.SaveTaxiPool(taxipools);
        //    }
        //    else
        //        return 0;
        //}
        //[HttpPut("/updatetaxipool/{taxipools}", Name = "UpdateTaxiPool")]
        //public ActionResult<int> UpdateTaxiPool(TaxiPools taxipools)
        //{
        //    if (taxipools is not null)
        //        return _taxiPoolRepositore.SaveTaxiPool(taxipools);
        //    else
        //        return NotFound();

        //}

        //[HttpDelete("/deltaxipool/{id}", Name = "DeleteTaxiPoolID")]
        //public async Task<ActionResult<bool>> DeleteTaxiPoolID(int id)
        //{
        //    if (id > 0)
        //    {
        //        var result = await _taxiPoolRepositore.DeleteTaxiPool(id);
        //        return result;
        //    }
        //    return BadRequest();
        //}
    }
}

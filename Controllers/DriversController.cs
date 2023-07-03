using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Repositore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriversRepositore _driversRepositore;
        public DriversController(IDriversRepositore driversRepositore) => _driversRepositore = driversRepositore;


        [HttpGet, Route("/GetSerch/{firstName}")]
        public ActionResult<IEnumerable<Drivers>>? GetDriversByName(string firstName, string? lastName, string? secondName, DateTime? dateTime)
        {
            var result = _driversRepositore.GetDriverByName(firstName, lastName, secondName, dateTime);
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }
        [HttpGet, Route("/dateRogden/{dateRogden}")]
        public ActionResult<IEnumerable<Drivers>>? GetDriversByDate(DateTime? dateRogden)
        {
            var result = _driversRepositore.GetDriverByDate(dateRogden);
            if (result is not null) return Ok(result);
            else return NotFound();
        }

        [HttpGet("/driverByID/{id}", Name = "GetDriverByID")]
        public ActionResult<Drivers> GetDriverByID(int id)
        {
            var result = _driversRepositore.GetDriversId(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost("/savedriver/{driverModels}")]
        public ActionResult<int> SaveDriver(Drivers driverModels)
        {
            if (driverModels is not null)
            {
                return _driversRepositore.SaveDriver(driverModels);
            }
            else
                return 0;
        }
        [HttpPut("/updatedriver/{driverModels}")]
        public ActionResult<int> UpdateDriverModels(Drivers drivers)
        {
            if (drivers is not null)
                return _driversRepositore.SaveDriver(drivers);
            else
                return NotFound();

        }

        [HttpDelete("/deldriver/{id}", Name = "DeleteDriverID")]
        public async Task<ActionResult<bool>> DeleteDriverID(int id)
        {
            if (id>0)
            {
                var result = await _driversRepositore.DeleteDriver(id);
                return result;
            }
            return BadRequest();
        }
    }
}

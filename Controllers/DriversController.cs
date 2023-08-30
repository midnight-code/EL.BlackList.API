using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Repositore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriversServices _driversServices;
        public DriversController(IDriversServices driversServices) => _driversServices = driversServices;

        [HttpGet, Route("/GetSerch/{firstName}", Name = "GetDriversByName")]
        public async Task<ActionResult<IEnumerable<Drivers>>?> GetDriversByName(string firstName, string? lastName, string? secondName, DateTime? dateTime)
        {
            var result = await _driversServices.GetDriversByNameAsync(firstName, lastName, secondName, dateTime);
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }

        [HttpGet, Route("/dateRogden/{dateRogden}", Name = "GetDriversByDate")]
        public async Task<ActionResult<IEnumerable<Drivers>>?> GetDriversByDate(DateTime? dateRogden)
        {
            var result = await _driversServices.GetDriversByDateAsync(dateRogden);
            if (result is not null) return Ok(result);
            else return NotFound();
        }

        [HttpGet("/driverByID/{id}", Name = "GetDriverByID")]
        public async Task<ActionResult<Drivers>> GetDriverByID(int id)
        {
            var result = await _driversServices.GetDrivreByIdAsync(id);
            if (result is not null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost("/savedriver/{driverModels}", Name = "SaveDriver")]
        public async Task<ActionResult<int>> SaveDriver(Drivers driverModels)
        {
            if (driverModels is not null)
            {
                return Ok(await _driversServices.SaveDriverAsync(driverModels));
            }
            else
                return 0;
        }
        [HttpPut("/updatedriver/{driverModels}", Name = "UpdateDriverModels")]
        public async Task<ActionResult<int>> UpdateDriverModels(Drivers drivers)
        {
            if (drivers is not null)
                return Ok(await _driversServices.SaveDriverAsync(drivers));
            else
                return NotFound();

        }

        [HttpDelete("/deldriver/{id}", Name = "DeleteDriverID")]
        public async Task<ActionResult<bool>> DeleteDriverID(int id)
        {
            if (id > 0)
            {
                return Ok(await _driversServices.DeleteDriverBAsync(id));
            }
            return BadRequest();
        }
    }
}

using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.BlackList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices _cityServices;
        public CityController(ICityServices cityServices) => _cityServices = cityServices;

        [HttpGet("/city/GetCity", Name ="GetCity")]
        public async Task<ActionResult<IEnumerable<City>>> GetCity() {
            return Ok(await _cityServices.GetCityAsync());
        }

        [HttpGet("/city/GetCityById/{id:int}", Name = "GetCityById")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            if (id > 0)
            {
                return Ok(await _cityServices.GetCityByIdAsync(id));
            }
            return BadRequest();
        }

        [HttpGet("/city/GetCityByName/{name}", Name ="GetCityByName")]
        public async Task<ActionResult<City>> GetCityByName(string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
                return Ok(await _cityServices.GetCityByNameAsync(name));
            return BadRequest();
        }

        [HttpPost("/city/SaveCity/{city}", Name ="SaveCity")]
        public async Task<ActionResult<int>> SaveCity(City city)
        {
            if(city is not null)
                return Ok(await _cityServices.SaveCityAsync(city));
            return BadRequest();
        }
        [HttpDelete("/city/deletecity/{id:int}", Name ="DeleteCity")]
        public async Task<ActionResult<bool>> DeleteCity(int id)
        {
            if(id>0)
                return Ok(await _cityServices.DeleteCityBAsync(id));
            return BadRequest();
        }
    }
}

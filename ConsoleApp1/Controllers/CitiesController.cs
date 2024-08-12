using ConsoleApp1.Models;
using ConsoleApp1.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var result = cityService.GetCities();
            return Ok(result.Result);
        }

        [HttpGet("GetCityByName/{name}")]
        public IActionResult GetCityByName(string name)
        {
            City city = cityService.GetCityByName(name);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPut("UpdateTransportCost")]
        public IActionResult UpdateTransportCost([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest("Object city is null");
            }

            var response = cityService.UpdateTransportCost(city.Name, city.TransportCost);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("UpdateCostOfWorkingHour")]
        public IActionResult UpdateCostOfWorkingHour([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest("Object city is null");
            }

            var response = cityService.UpdateCostOfWorkingHour(city.Name, city.CostOfWorkingHour);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("AddCity")]
        public IActionResult AddCity([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest("Object city is null");
            }

            var response = cityService.AddCity(city);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest("Error");
        }

        [HttpDelete("DeleteCity/{name}")]
        public IActionResult DeleteCity(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("City name is null");
            }

            var response = cityService.DeleteCity(name);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest("Error");
        }
    }
}

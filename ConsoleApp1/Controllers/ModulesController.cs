using ConsoleApp1.Models;
using ConsoleApp1.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService moduleService;

        public ModulesController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        [HttpGet("GetModules")]
        public IActionResult GetModules()
        {
            var result = moduleService.GetModules();
            return Ok(result.Result);
        }

        [HttpGet("GetModulesByName/{name}")]
        public IActionResult GetModulesByName(string name)
        {
            var module = moduleService.GetModuleByName(name);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        [HttpPut("UpdateModule")]
        public IActionResult UpdateModule([FromBody] Module module)
        {
            if (module == null)
            {
                return BadRequest("Object module is null");
            }

            var response = moduleService.UpdateModule(module);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("AddModule")]
        public IActionResult AddModule([FromBody] Module module)
        {
            if (module == null)
            {
                return BadRequest("Object module is null");
            }

            var response = moduleService.AddModule(module);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest("Error");
        }

        [HttpDelete("DeleteModule/{name}")]
        public IActionResult DeleteModule(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Module name is null");
            }

            var response = moduleService.DeleteModule(name);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest("Error");
        }
    }
}

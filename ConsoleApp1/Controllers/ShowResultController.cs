using ConsoleApp1.Models;
using ConsoleApp1.ModelsDTO;
using ConsoleApp1.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowResultController : ControllerBase
    {
        private readonly ICalculatorCostService calculatorCostService;

        public ShowResultController(ICalculatorCostService calculatorCostService)
        {
            this.calculatorCostService = calculatorCostService;
        }

        [HttpPost("GetCost")]
        public IActionResult GetCost([FromBody] ShowResultDTO showResultDTO)
        {
            if (showResultDTO == null)
            {
                return BadRequest("Object showResultDTO is null");
            }

            var result = calculatorCostService.CalculateCost(showResultDTO.CityName, showResultDTO.ModuleListDTO);

            if (result is OperationSuccessDTO<ResultCostDTO> success)
            {
                return Ok(success.Result);
            }

            return BadRequest((result as OperationErrorDTO)?.Message);
        }
    }
}

using ConsoleApp1.Models;
using ConsoleApp1.ModelsDTO;
using ConsoleApp1.services.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchHistoriesController : ControllerBase
    {
        private readonly ISearchHistoryService searchHistoryService;

        public SearchHistoriesController(ISearchHistoryService searchHistoryService)
        {
            this.searchHistoryService = searchHistoryService;
        }

        [HttpGet("GetSearchHistory")]
        public IActionResult GetSearchHistory([FromQuery] string cityName, [FromQuery] ModuleListDTO moduleListDTO)
        {
            var result = searchHistoryService.GetSearchHistory(cityName, moduleListDTO);
            if (result.InSearchHistory)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("AddSearchHistory")]
        public IActionResult AddSearchHistory([FromBody] SearchHistory searchHistory)
        {
            if (searchHistory == null)
            {
                return BadRequest("Object searchHistory is null");
            }

            var response = searchHistoryService.AddSearchHistory(searchHistory);

            if (response.Message.Equals("Success"))
            {
                return Ok(response.Message);
            }
            return BadRequest("Error");
        }
    }
}

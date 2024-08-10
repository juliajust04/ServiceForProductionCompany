using ConsoleApp1.services.interfaces;
using ConsoleApp1.Models;
using ConsoleApp1.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.services.implementations
{
    public class ShowResultService : IShowResultService
    {
        private readonly ISearchHistoryService searchHistoryService;
        private readonly ICalculatorCostService calculatorCostService;
        private readonly ICityService cityService;

        public ShowResultService(ISearchHistoryService searchHistoryService, ICalculatorCostService calculatorCostService, ICityService cityService)
        {
            this.searchHistoryService = searchHistoryService;
            this.calculatorCostService = calculatorCostService;
            this.cityService = cityService;
        }

        public ResultCostDTO PresentResult(string cityName, ModuleListDTO moduleListDTO)
        {
            var checkInHistory = searchHistoryService.GetSearchHistory(cityName, moduleListDTO);
            OperationSuccessDTO<ResultCostDTO> calculateCost = null;

            if(checkInHistory != null)
            {
                return new ResultCostDTO
                {
                    Cost = checkInHistory.Cost,
                    InSearchHistory =
                    checkInHistory.InSearchHistory
                };
            }
            try
            {
                calculateCost = (OperationSuccessDTO<ResultCostDTO>)
                    calculatorCostService.CalculateCost(cityName, moduleListDTO);
            }
            catch
            {
                return new ResultCostDTO { Cost = -1, InSearchHistory = false };
            }
            var city = cityService.GetCityByName(cityName);

            SearchHistory searchHistory = new SearchHistory
            {
                CityId = city.Id,
                ProductionCost = calculateCost.Result.Cost,
                ModuleName1 = moduleListDTO.ModuleList.Count > 0 ?
                moduleListDTO.ModuleList[0] : string.Empty,
                ModuleName2 = moduleListDTO.ModuleList.Count > 0 ?
                moduleListDTO.ModuleList[1] : string.Empty,
                ModuleName3 = moduleListDTO.ModuleList.Count > 0 ?
                moduleListDTO.ModuleList[2] : string.Empty,
                ModuleName4 = moduleListDTO.ModuleList.Count > 0 ?
                moduleListDTO.ModuleList[3] : string.Empty,
            };

            searchHistoryService.AddSearchHistory(searchHistory);
            return new ResultCostDTO { Cost = calculateCost.Result.Cost,
                InSearchHistory = false };
        }
    }
}

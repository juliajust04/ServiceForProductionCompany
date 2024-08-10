using ConsoleApp1.Models;
using ConsoleApp1.ModelsDTO;
using ConsoleApp1.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.services.implementations
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private readonly CalculatorContext context;
        private readonly IModuleService moduleService;
        private readonly ICityService cityService;

        public SearchHistoryService(CalculatorContext context, IModuleService moduleService,
            ICityService cityService)
        {
            this.context = context;
            this.moduleService = moduleService;
            this.cityService = cityService;
        }

        public OperationResultDTO AddSearchHistory(SearchHistory searchHistory)
        {
            context.SearchHistory.Add(searchHistory);
            context.SaveChanges();
            return new OperationSuccessDTO<Module>
            {
                Message = "Success"
            };
        }

        public ResultCostDTO GetSearchHistory(string cityName, ModuleListDTO moduleListDTO)
        {
            var city = cityService.GetCityByName(cityName);
            if (city == null)
            {
                return new ResultCostDTO { InSearchHistory = false };
            }

            var searchHistories = context.SearchHistory.Where(sh => sh.CityId == city.Id).ToList();
            if (!searchHistories.Any())
            {
                return new ResultCostDTO { InSearchHistory = false };
            }

            foreach (var searchHistory in searchHistories)
            {
                int counterModule = 0;
                foreach (var module in moduleListDTO.ModuleList)
                {
                    if (searchHistory.ModuleName1 == module ||
                        searchHistory.ModuleName2 == module ||
                        searchHistory.ModuleName3 == module ||
                        searchHistory.ModuleName4 == module)
                    {
                        counterModule++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (moduleListDTO.ModuleList.Count == ModuleHasValue(searchHistory) &&
                    moduleListDTO.ModuleList.Count == counterModule)
                {
                    return new ResultCostDTO
                    {
                        InSearchHistory = true,
                        Cost = searchHistory.ProductionCost
                    };
                }
            }
            return new ResultCostDTO { InSearchHistory = false };
        }

        OperationSuccessDTO<IList<SearchHistory>> ISearchHistoryService.GetSearchHistories()
        {
            var searchHistories = context.SearchHistory.ToList();
            return new OperationSuccessDTO<IList<SearchHistory>>
            {
                Message = "Success",
                Result = searchHistories
            };
        }

        private int ModuleHasValue(SearchHistory searchHistory)
        {
            int counter = 0;
            if (!string.IsNullOrEmpty(searchHistory.ModuleName1)) counter++;
            if (!string.IsNullOrEmpty(searchHistory.ModuleName2)) counter++;
            if (!string.IsNullOrEmpty(searchHistory.ModuleName3)) counter++;
            if (!string.IsNullOrEmpty(searchHistory.ModuleName4)) counter++;
            return counter;
        }
    }
}

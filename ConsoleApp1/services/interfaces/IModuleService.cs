using ConsoleApp1.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.services.interfaces
{
    public interface IModuleService
    {
        Module GetModuleByName(string moduleName);
        OperationSuccessDTO<List<Module>> GetModules();
        OperationSuccessDTO<Module> AddModule(Module module);
        OperationSuccessDTO<Module> UpdateModule(Module module);
        OperationSuccessDTO<Module> DeleteModule(string name);

    }
}

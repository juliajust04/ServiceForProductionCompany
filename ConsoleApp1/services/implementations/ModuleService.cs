using ConsoleApp1.Models;
using ConsoleApp1.ModelsDTO;
using ConsoleApp1.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.services.implementations
{
    public class ModuleService : IModuleService
    {
        private readonly CalculatorContext context;
        public ModuleService(CalculatorContext context)
        {

            this.context = context;
        }
        public OperationSuccessDTO<Module> AddModule(Module module)
        {
            context.Module.Add(module);
            context.SaveChanges();
            return new OperationSuccessDTO<Module> { Message = "Success" };
        }
        public Module GetModuleByName(string moduleName)
        {
            return context.Module.Where(module => module.Name == moduleName).FirstOrDefault();

        }
        public OperationSuccessDTO<List<Module>> GetModules()
        {
            List<Module> modules = context.Module.ToList();
            return new OperationSuccessDTO<List<Module>>
            {
                Message = "Success",
                Result = modules
            };
        }
        public OperationSuccessDTO<Module> DeleteModule(string name)
        {
            var module = context.Module.Where(m => m.Name == name).FirstOrDefault();
            context.Module.Remove(module);
            context.SaveChanges();
            return new OperationSuccessDTO<Module> { Message = "Success" };
        }
        public OperationSuccessDTO<Module> UpdateModule(Module module)
        {
            var mod = context.Module.Where(m => m.Name == module.Name).FirstOrDefault();
            mod.Name = module.Name;
            mod.Price = module.Price;
            mod.Description = module.Description;
            mod.Weight = module.Weight;
            mod.AssemblyTime = module.AssemblyTime;
            mod.Code = module.Code;
            context.SaveChanges();
            return new OperationSuccessDTO<Module> { Message = "Success" };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace ConsoleApp1.Models
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options) { }

        public DbSet<SearchHistory> SearchHistory { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<City> City { get; set; }
    }
}

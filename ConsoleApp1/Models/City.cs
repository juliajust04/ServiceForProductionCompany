using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TransportCost { get; set; }
        public double CostOfWorkingHour { get; set; }
        public virtual SearchHistory SearchHistory { get; set; }
    }
}

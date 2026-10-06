using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElecticityUsage
{
    public class PowerUsage
    {
        public DateTime Date { get; set; }
        public string Appliance { get; set; }
        public double HoursUsed { get; set; }
        public int PowerWatts { get; set; }
        public double EnergyKWh { get; set; }
    }
}

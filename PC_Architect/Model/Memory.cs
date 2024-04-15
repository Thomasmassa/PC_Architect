using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Memory : IComponent
    {
        public string Image { get; set; } = "";
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public List<int> Speed { get; set; } = new List<int>();
        public List<int> Modules { get; set; } = new List<int>();
        public double PricePerGb { get; set; }
        public string Color { get; set; } = "";
        public double FirstWordLatency { get; set; }
        public int CasLatency { get; set; }
    }
}

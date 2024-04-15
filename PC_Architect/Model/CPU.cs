using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    public class Cpu : IComponent
    {
        public string Image { get; set; } = "";
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public int CoreCount { get; set; }
        public double Core_clock { get; set; }
        public double BoostClock { get; set; }
        public int Tdp { get; set; }
        public string Graphics { get; set; } = "";
        public bool Smt { get; set; }
        public string Docket { get; set; } = "";
    }
}

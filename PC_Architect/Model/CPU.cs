using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    public class CPU
    {
        public double Boost_clock { get; set; }
        public double Core_clock { get; set; }  
        public int Core_count { get; set; }
        public string Graphics { get; set; } = "";
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool Smt { get; set; }
        public int Temp { get; set; }   
    }
}

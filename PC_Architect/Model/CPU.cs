using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    public class Cpu
    {
        public string name { get; set; }
        public double price { get; set; }
        public int core_count { get; set; }
        public double core_clock { get; set; }
        public double boost_clock { get; set; }
        public int tdp { get; set; }
        public string graphics { get; set; }
        public bool smt { get; set; }
        public string socket { get; set; }
        public string img { get; set; }
    }
}

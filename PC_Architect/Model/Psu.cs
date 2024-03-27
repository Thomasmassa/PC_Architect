using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Psu
    {
        public string name { get; set; }
        public double? price { get; set; }
        public string type { get; set; }
        public string efficiency { get; set; }
        public int wattage { get; set; }
        public object modular { get; set; }
        public string color { get; set; }
    }
}

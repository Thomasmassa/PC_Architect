using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Gpu
    {
        public string name { get; set; }
        public double? price { get; set; }
        public string chipset { get; set; }
        public int memory { get; set; }
        public int core_clock { get; set; }
        public int? boost_clock { get; set; }
        public string color { get; set; }
        public int length { get; set; }
    }
}

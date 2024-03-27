using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Gpu
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Chipset { get; set; }
        public int Memory { get; set; }
        public int CoreClock { get; set; }
        public int? BoostClock { get; set; }
        public string Color { get; set; }
        public int Length { get; set; }
    }
}

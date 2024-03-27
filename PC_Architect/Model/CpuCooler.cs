using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class CpuCooler
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public object Rpm { get; set; }
        public object NoiseLevel { get; set; }
        public string Color { get; set; }
        public int? Size { get; set; }
    }
}

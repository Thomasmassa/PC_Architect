using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class ExternalStorage
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Type { get; set; }
        public string Interface { get; set; }
        public int Capacity { get; set; }
        public double? PricePerGb { get; set; }
        public string Color { get; set; } = "";
    }
}

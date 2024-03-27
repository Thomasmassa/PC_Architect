using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class ExternalStorage
    {
        public string name { get; set; }
        public double? price { get; set; }
        public string type { get; set; }
        public string @interface { get; set; }
        public int capacity { get; set; }
        public double? price_per_gb { get; set; }
        public string color { get; set; } = "";
    }
}

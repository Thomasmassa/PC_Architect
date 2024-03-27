using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class InternalStorage
    {
        public string name { get; set; }
        public double? price { get; set; }
        public int capacity { get; set; }
        public double? price_per_gb { get; set; }
        public object type { get; set; }
        public int? cache { get; set; }
        public object form_factor { get; set; }
        public string @interface { get; set; }
    }
}

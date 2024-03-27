using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class InternalStorage : IBindable
    {
        public string ImageSource { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int Capacity { get; set; }
        public double? PricePerGb { get; set; }
        public object Type { get; set; }
        public int? Cache { get; set; }
        public object FormFactor { get; set; }
        public string Interface { get; set; }
    }
}

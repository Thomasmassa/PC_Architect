using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Case
    {
        public string name { get; set; }
        public double? price { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public object psu { get; set; }
        public string side_panel { get; set; }
        public double? external_volume { get; set; }
        public int internal_35_bays { get; set; }
    }
}

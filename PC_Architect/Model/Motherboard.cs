using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Motherboard
    {
        public string name { get; set; }
        public double price { get; set; }
        public string socket { get; set; }
        public string form_factor { get; set; }
        public int max_memory { get; set; }
        public int memory_slots { get; set; }
        public string color { get; set; }
    }
}

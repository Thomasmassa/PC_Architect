using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Memory
    {
        public string name { get; set; }
        public double price { get; set; }
        public List<int> speed { get; set; }
        public List<int> modules { get; set; }
        public double price_per_gb { get; set; }
        public string color { get; set; }
        public double first_word_latency { get; set; }
        public int cas_latency { get; set; }
    }
}

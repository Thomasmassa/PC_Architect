using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Motherboard : IComponent
    {
        public string Image { get; set; } = "";
        public string Name { get; set; } = "";  
        public double Price { get; set; }
        public string Socket { get; set; } = "";
        public string FormFactor { get; set; } = "";
        public int MaxMemory { get; set; }
        public int MemorySlots { get; set; }
        public string Color { get; set; } = "";
    }
}

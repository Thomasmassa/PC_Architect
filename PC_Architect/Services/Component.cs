using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Services
{
    internal class Component : IComponent
    {
        public string Name { get; set; } = "";
        public string Image { get; set; } = "";
        public double? Price { get; set; }
    }
}

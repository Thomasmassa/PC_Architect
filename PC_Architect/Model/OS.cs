using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class OS : IBindable
    {
        public string Image { get; set; } = "";
        public string Name { get; set; } = "";
        public double? Price { get; set; }
        public object Mode { get; set; } = "";
        public int MaxMemory { get; set; }
    }
}

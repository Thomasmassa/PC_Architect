using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Case : IBindable
    {
        public string ImageSource { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public object Psu { get; set; }
        public string SidePanel { get; set; }
        public double? ExternalVolume { get; set; }
        public int Internal35Bays { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Psu
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Type { get; set; }
        public string Efficiency { get; set; }
        public int Wattage { get; set; }
        public object Modular { get; set; }
        public string Color { get; set; }
    }
}

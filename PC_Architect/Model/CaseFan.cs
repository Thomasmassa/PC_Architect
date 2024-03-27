using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class CaseFan
    {
        public string name { get; set; }
        public double? price { get; set; }
        public int size { get; set; }
        public string color { get; set; }
        public object rpm { get; set; }
        public object airflow { get; set; }
        public object noise_level { get; set; }
        public bool pwm { get; set; } = false;
    }
}

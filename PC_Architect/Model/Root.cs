using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Root
    {
        public List<Cpu> Cpu { get; set; }
        public List<CpuCooler> CpuCooler { get; set; }
        public List<Motherboard> Motherboard { get; set; }
        public List<Memory> Memory { get; set; }
        public List<Gpu> Gpu { get; set; }
        public List<InternalStorage> InternalStorage { get; set; }
        public List<ExternalStorage> ExternalStorage { get; set; }
        public List<Psu> Psu { get; set; }
        public List<Case> Case { get; set; }
        public List<CaseFan> Case_Fan { get; set; }
        public List<OS> Os { get; set; }
    }
}

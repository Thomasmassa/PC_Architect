
namespace PC_Architect.Model
{
    class Root
    {
        public List<Cpu> Cpu { get; set; } = new List<Cpu>();
        public List<CpuCooler> CpuCooler { get; set; } = new List<CpuCooler>();
        public List<Motherboard> Motherboard { get; set; } = new List<Motherboard>();
        public List<Memory> Memory { get; set; } = new List<Memory>();
        public List<Gpu> Gpu { get; set; } = new List<Gpu>();
        public List<InternalStorage> InternalStorage { get; set; } = new List<InternalStorage>();
        public List<ExternalStorage> ExternalStorage { get; set; } = new List<ExternalStorage>();
        public List<Psu> Psu { get; set; } = new List<Psu>();
        public List<Case> Case { get; set; } = new List<Case>();
        public List<CaseFan> Case_Fan { get; set; } = new List<CaseFan>();
        public List<OS> Os { get; set; } = new List<OS>();
    }
}

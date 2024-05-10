// ROOT BEVAT LIJSTEN VAN VERSCHILLENDE TYPE EIGENSCHAPPEN 

namespace PcArchitect.Model
{
    public class Root
    {
        public List<Cpu> Cpu { get; set; }
        public List<CpuCooler> CpuCooler { get; set; } 
        public List<Motherboard> Motherboard { get; set; } 
        public List<Memory> Memory { get; set; } 
        public List<Gpu> Gpu { get; set; } 
        public List<Storage> Storage { get; set; } 
        public List<Psu> Psu { get; set; } 
        public List<Case> Case { get; set; } 
        public List<CaseFan> Case_Fan { get; set; } 
        public List<Os> Os { get; set; } 

        // DEZE CONSTRUCTOR WORDT GEBRUIKT OM DE 0'DE POSITIE VAN DE LIJSTEN TE VULLEN MET EEN STANDAARD WAARDE
        // DEZE STANDAARD WAARDEN WORDEN GETOOND IN DE XAML STARTBUILDINGPAGE ALS DE GEBRUIKER NOG NIETS HEEFT GESELECTEERD

        public Root()
        {
            Cpu = new List<Cpu> { new Cpu { Name = "CPU", Image = "cpu.png", IsPresetFrameEnabled = true } };
            CpuCooler = new List<CpuCooler> { new CpuCooler { Name = "CPU COOLER", Image = "cpu_cooler.png", IsPresetFrameEnabled = true } };
            Gpu = new List<Gpu> { new Gpu { Name = "GPU", Image = "gpu.png", IsPresetFrameEnabled = true } };
            Motherboard = new List<Motherboard> { new Motherboard { Name = "MOTHERBOARD", Image = "motherboard.png", IsPresetFrameEnabled = true } };
            Memory = new List<Memory> { new Memory { Name = "MEMORY", Image = "memory.png", IsPresetFrameEnabled = true } };
            Storage = new List<Storage> { new Storage { Name = "STORAGE", Image = "ssd.png", IsPresetFrameEnabled = true } };
            Psu = new List<Psu> { new Psu { Name = "PSU", Image = "psu.png", IsPresetFrameEnabled = true } };
            Case_Fan = new List<CaseFan> { new CaseFan { Name = "CASE FAN", Image = "case_fan.png", IsPresetFrameEnabled = true } };
            Case = new List<Case> { new Case { Name = "CASE", Image = "case_tower.png", IsPresetFrameEnabled = true } };
            Os = new List<Os> { new Os { Name = "OS", Image = "os.png", IsPresetFrameEnabled = true } };
        }
    }
}
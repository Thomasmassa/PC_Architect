using System.ComponentModel;

namespace PcArchitect.Model
{
    public class Root
    {
        public List<Cpu> Cpu { get; set; } = new List<Cpu>();
        public List<CpuCooler> CpuCooler { get; set; } = new List<CpuCooler>();
        public List<Motherboard> Motherboard { get; set; } = new List<Motherboard>();
        public List<Memory> Memory { get; set; } = new List<Memory>();
        public List<Gpu> Gpu { get; set; } = new List<Gpu>();
        public List<Ssd> Ssd { get; set; } = new List<Ssd>();
        public List<Hdd> Hdd { get; set; } = new List<Hdd>();
        public List<Psu> Psu { get; set; } = new List<Psu>();
        public List<Case> Case { get; set; } = new List<Case>();
        public List<CaseFan> Case_Fan { get; set; } = new List<CaseFan>();
        public List<Os> Os { get; set; } = new List<Os>();

        public Root(){
            Cpu = new List<Cpu>{new Cpu { Name = "CPU", Image = "cpu.png" , IsPresetFrameEnabled = true } };
            CpuCooler = new List<CpuCooler> { new CpuCooler { Name = "CPU COOLER" , Image = "cpu_cooler.png", IsPresetFrameEnabled = true } };
            Gpu = new List<Gpu> { new Gpu { Name = "GPU", Image = "gpu.png" , IsPresetFrameEnabled = true } };
            Motherboard = new List<Motherboard> { new Motherboard { Name = "MOTHERBOARD" , Image = "motherboard.png", IsPresetFrameEnabled = true } };
            Memory = new List<Memory> { new Memory { Name = "MEMORY", Image = "memory.png" , IsPresetFrameEnabled = true } };
            Ssd = new List<Ssd> { new Ssd { Name = "SSD", Image = "ssd.png" , IsPresetFrameEnabled = true } };
            Hdd = new List<Hdd> { new Hdd { Name = "HDD", Image = "hdd.png" , IsPresetFrameEnabled = true } };
            Psu = new List<Psu> { new Psu { Name = "PSU", Image = "psu.png" , IsPresetFrameEnabled = true } };
            Case_Fan = new List<CaseFan> { new CaseFan { Name = "CASE FANS", Image = "case_fan.png" , IsPresetFrameEnabled = true } };
            Case = new List<Case> { new Case { Name = "CASE", Image = "case_tower.png" , IsPresetFrameEnabled = true } };
            Os = new List<Os> { new Os { Name = "OS", Image = "os.png" , IsPresetFrameEnabled = true } };
        }
    }
}

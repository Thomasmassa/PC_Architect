﻿namespace PcArchitect.Model
{
    public class Root
    {
        public List<Cpu> Cpu { get; set; } = [];
        public List<CpuCooler> CpuCooler { get; set; } = [];
        public List<Motherboard> Motherboard { get; set; } = [];
        public List<Memory> Memory { get; set; } = [];
        public List<Gpu> Gpu { get; set; } = [];
        public List<Ssd> InternalStorage { get; set; } = [];
        public List<Hdd> ExternalStorage { get; set; } = [];
        public List<Psu> Psu { get; set; } = [];
        public List<Case> Case { get; set; } = [];
        public List<CaseFan> Case_Fan { get; set; } = [];
        public List<Os> Os { get; set; } = [];
    }
}
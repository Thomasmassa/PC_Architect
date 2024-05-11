﻿// ROOT BEVAT LIJSTEN VAN VERSCHILLENDE TYPE EIGENSCHAPPEN 

namespace PcArchitect.Model
{
    public class Root
    {
        public List<Cpu> Cpu { get; set; } = [];
        public List<CpuCooler> CpuCooler { get; set; } = [];
        public List<Motherboard> Motherboard { get; set; } = [];
        public List<Memory> Memory { get; set; } = [];
        public List<Gpu> Gpu { get; set; } = [];
        public List<Storage> Storage { get; set; } = []; 
        public List<Psu> Psu { get; set; } = [];
        public List<Case> Case { get; set; } = [];
        public List<CaseFan> Case_Fan { get; set; } = []; 
        public List<Os> Os { get; set; } = [];

        // DEZE CONSTRUCTOR WORDT GEBRUIKT OM DE 0'DE POSITIE VAN DE LIJSTEN TE VULLEN MET EEN STANDAARD WAARDE
        // DEZE STANDAARD WAARDEN WORDEN GETOOND IN DE XAML STARTBUILDINGPAGE ALS DE GEBRUIKER NOG NIETS HEEFT GESELECTEERD
    }
}
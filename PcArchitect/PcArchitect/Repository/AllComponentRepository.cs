using PcArchitect.Interfaces;
using PcArchitect.Model;

// WANNEER EEN INSTANTIE VAN DE KLASSE WORDT AANGEMAAKT, WORDEN ALLE COMPONENTEN UIT DE DATABASE ASYNCHROON OPGEHAALD
// EN TOEGEVOEGD AAN DE SPECIFIEKE LIJSTEN IN DE ROOT KLASSE
// DEZE KLASSE WORDT GEBRUIKT OM ALLE COMPONENTEN UIT DE DATABASE OP TE HALEN

namespace PcArchitect.Repository
{
    public class AllComponentRepository
    {
        private readonly IComponentService _comServ;
        private readonly RootFactory _rootF;

        public AllComponentRepository(IComponentService comServ, RootFactory rootF)
        {
            _comServ = comServ;
            _rootF = rootF;
        }

        public Task GetAllComponentsAsync()
        {
            Task.Run(async () =>
            {
                _rootF.GetRoot1().Cpu.AddRange(await _comServ.GetComponentsAsync<Cpu>("cpu"));
                _rootF.GetRoot1().CpuCooler.AddRange(await _comServ.GetComponentsAsync<CpuCooler>("cpucooler"));
                _rootF.GetRoot1().Motherboard.AddRange(await _comServ.GetComponentsAsync<Motherboard>("motherboard"));
                _rootF.GetRoot1().Gpu.AddRange(await _comServ.GetComponentsAsync<Gpu>("gpu"));
                _rootF.GetRoot1().Memory.AddRange(await _comServ.GetComponentsAsync<Memory>("memory"));
                _rootF.GetRoot1().Ssd.AddRange(await _comServ.GetComponentsAsync<Ssd>("ssd"));
                _rootF.GetRoot1().Hdd.AddRange(await _comServ.GetComponentsAsync<Hdd>("hdd"));
                _rootF.GetRoot1().Psu.AddRange(await _comServ.GetComponentsAsync<Psu>("psu"));
                _rootF.GetRoot1().Case.AddRange(await _comServ.GetComponentsAsync<Case>("case"));
                _rootF.GetRoot1().Case_Fan.AddRange(await _comServ.GetComponentsAsync<CaseFan>("casefan"));
                _rootF.GetRoot1().Os.AddRange(await _comServ.GetComponentsAsync<Os>("os"));
            });
            return Task.CompletedTask;
        }
    }
}

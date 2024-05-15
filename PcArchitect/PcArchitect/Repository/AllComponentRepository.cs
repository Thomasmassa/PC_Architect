using PcArchitect.Interfaces;
using PcArchitect.Model;

/*
De AllComponentRepository klasse is verantwoordelijk voor het ophalen en toevoegen van alle componenten aan de Root klasse.

De IComponentService wordt gebruikt om de componenten op te halen en de RootFactory wordt gebruikt om de Root klasse te verkrijgen.

De GetAllComponentsAsync methode wordt aangeroepen om alle componenten op te halen.
Omdat de actie asynchrone methoden bevat (GetComponentsAsync), wordt async gebruikt om aan te geven dat de actie asynchroon is.
*/

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
            return Task.Run(async () =>
            {
                _rootF.GetRoot1().Cpu.AddRange(await _comServ.GetComponentsAsync<Cpu>("cpu"));
                _rootF.GetRoot1().CpuCooler.AddRange(await _comServ.GetComponentsAsync<CpuCooler>("cpucooler"));
                _rootF.GetRoot1().Motherboard.AddRange(await _comServ.GetComponentsAsync<Motherboard>("motherboard"));
                _rootF.GetRoot1().Gpu.AddRange(await _comServ.GetComponentsAsync<Gpu>("gpu"));
                _rootF.GetRoot1().Memory.AddRange(await _comServ.GetComponentsAsync<Memory>("memory"));
                _rootF.GetRoot1().Storage.AddRange(await _comServ.GetComponentsAsync<Storage>("ssd"));
                _rootF.GetRoot1().Psu.AddRange(await _comServ.GetComponentsAsync<Psu>("psu"));
                _rootF.GetRoot1().Case.AddRange(await _comServ.GetComponentsAsync<Case>("case"));
                _rootF.GetRoot1().Case_Fan.AddRange(await _comServ.GetComponentsAsync<CaseFan>("casefan"));
                _rootF.GetRoot1().Os.AddRange(await _comServ.GetComponentsAsync<Os>("os"));
            });
        }
    }
}

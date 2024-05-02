using PcArchitect.Interfaces;
using PcArchitect.Model;

namespace PcArchitect.Repository
{
    public class AllComponentRepository
    {
        private readonly Root _rootAllComponents = new Root();
        private readonly IComponentService _componentService;

        public AllComponentRepository(Root rootAllComponents, IComponentService componentService)
        {
            _rootAllComponents = rootAllComponents;
            _componentService = componentService;


            GetAllComponentsAsync();
        }

        public Task GetAllComponentsAsync()
        {
            Task.Run(async () =>
            {
                _rootAllComponents.Cpu.AddRange(await _componentService.GetComponentsAsync<Cpu>("cpu"));
                _rootAllComponents.CpuCooler.AddRange(await _componentService.GetComponentsAsync<CpuCooler>("cpucooler"));
                _rootAllComponents.Motherboard.AddRange(await _componentService.GetComponentsAsync<Motherboard>("motherboard"));
                _rootAllComponents.Gpu.AddRange(await _componentService.GetComponentsAsync<Gpu>("gpu"));
                _rootAllComponents.Memory.AddRange(await _componentService.GetComponentsAsync<Memory>("memory"));
                _rootAllComponents.Ssd.AddRange(await _componentService.GetComponentsAsync<Ssd>("ssd"));
                _rootAllComponents.Hdd.AddRange(await _componentService.GetComponentsAsync<Hdd>("hdd"));
                _rootAllComponents.Psu.AddRange(await _componentService.GetComponentsAsync<Psu>("psu"));
                _rootAllComponents.Case.AddRange(await _componentService.GetComponentsAsync<Case>("case"));
                _rootAllComponents.Case_Fan.AddRange(await _componentService.GetComponentsAsync<CaseFan>("casefan"));
                _rootAllComponents.Os.AddRange(await _componentService.GetComponentsAsync<Os>("os"));
            });
            return Task.CompletedTask;
        }
    }
}

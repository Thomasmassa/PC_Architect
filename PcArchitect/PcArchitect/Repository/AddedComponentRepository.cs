using PcArchitect.Model;

// AFHANKELIJK VAN WELKE METHODE WORDT AANGEROEPEN, WORDT EEN COMPONENT TOEGEVOEGD OF VERWIJDERD 
// UIT DE SPECIFIEKE TYPE LIJST IN DE ROOT KLASSE

namespace PC_Architect.Model
{
    public class AddedComponentRepository
    {
        private readonly RootFactory _rootF;
        public AddedComponentRepository(RootFactory rootF)
        {
            _rootF = rootF;
        }

        public Task AddComponentAsync<T>(T component)
        {
            Task.Run(() =>
            {
                switch (component)
                {
                    case Cpu cpu:
                        _rootF.GetRoot2().Cpu.Add(cpu);
                        break;
                    case CpuCooler cpuCooler:
                        _rootF.GetRoot2().CpuCooler.Add(cpuCooler);
                        break;
                    case Gpu gpu:
                        _rootF.GetRoot2().Gpu.Add(gpu);
                        break;
                    case Motherboard motherboard:
                        _rootF.GetRoot2().Motherboard.Add(motherboard);
                        break;
                    case Memory memory:
                        _rootF.GetRoot2().Memory.Add(memory);
                        break;
                    case Ssd ssd:
                        _rootF.GetRoot2().Ssd.Add(ssd);
                        break;
                    case Hdd hdd:
                        _rootF.GetRoot2().Hdd.Add(hdd);
                        break;
                    case Psu psu:
                        _rootF.GetRoot2().Psu.Add(psu);
                        break;
                    case Case case_:
                        _rootF.GetRoot2().Case.Add(case_);
                        break;
                    case CaseFan caseFan:
                        _rootF.GetRoot2().Case_Fan.Add(caseFan);
                        break;
                    case Os os:
                        _rootF.GetRoot2().Os.Add(os);
                        break;


                }
            }); return Task.CompletedTask;
        }

        public async Task RemoveComponentAsync<T>(T component)
        {
            await Task.Run(() =>
            {
                switch (component)
                {
                    case Cpu cpu:
                        _rootF.GetRoot2().Cpu.Remove(cpu);
                        break;
                    case CpuCooler cpuCooler:
                        _rootF.GetRoot2().CpuCooler.Remove(cpuCooler);
                        break;
                    case Gpu gpu:
                        _rootF.GetRoot2().Gpu.Remove(gpu);
                        break;
                    case Motherboard motherboard:
                        _rootF.GetRoot2().Motherboard.Remove(motherboard);
                        break;
                    case Memory memory:
                        _rootF.GetRoot2().Memory.Remove(memory);
                        break;
                    case Ssd ssd:
                        _rootF.GetRoot2().Ssd.Remove(ssd);
                        break;
                    case Hdd hdd:
                        _rootF.GetRoot2().Hdd.Remove(hdd);
                        break;
                    case Psu psu:
                        _rootF.GetRoot2().Psu.Remove(psu);
                        break;
                    case Case case_:
                        _rootF.GetRoot2().Case.Remove(case_);
                        break;
                    case CaseFan caseFan:
                        _rootF.GetRoot2().Case_Fan.Remove(caseFan);
                        break;
                    case Os os:
                        _rootF.GetRoot2().Os.Remove(os);
                        break;
                }
            });
        }

        // VERWIJDER ALLE COMPONENTEN UIT DE ROOT2 KLASSE EN STANDAARDWAARDES WORDEN INGESTELD VOOR ALLE COMPONENTEN

        public void ClearComponents()
        {
            foreach (var property in _rootF.GetRoot2().GetType().GetProperties())
            {
                property.SetValue(_rootF.GetRoot2(), Activator.CreateInstance(property.PropertyType));
            }
        }
    }
}

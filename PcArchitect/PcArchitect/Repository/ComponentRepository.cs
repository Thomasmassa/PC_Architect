using PcArchitect.Model;
using System.ComponentModel;
using System.Xml.Linq;

namespace PC_Architect.Model
{
    public class ComponentRepository
    {
        private readonly Root _root;

        public ComponentRepository(Root root)
        {   
            _root = root;
        }

        public Task AddComponentAsync<T>(T component)
        {
            Task.Run(() =>
            { 
                switch (component)
                {
                    case Cpu cpu:
                        _root.Cpu.Add(cpu);
                        break;
                    case CpuCooler cpuCooler:
                        _root.CpuCooler.Add(cpuCooler);
                        break;
                    case Gpu gpu:
                        _root.Gpu.Add(gpu);
                        break;
                    case Motherboard motherboard:
                        _root.Motherboard.Add(motherboard);
                        break;
                    case Memory memory:
                        _root.Memory.Add(memory);
                        break;
                    case Ssd ssd:
                        _root.Ssd.Add(ssd);
                        break;
                    case Hdd hdd:
                        _root.Hdd.Add(hdd);
                        break;
                    case Psu psu:
                        _root.Psu.Add(psu);
                        break;
                    case Case case_:
                        _root.Case.Add(case_);
                        break;
                    case CaseFan caseFan:
                        _root.Case_Fan.Add(caseFan);
                        break;
                    case Os os:
                        _root.Os.Add(os);
                        break;


                }
            });return Task.CompletedTask;
        }

        public async Task RemoveComponentAsync<T>(T component)
        {
            await Task.Run(() =>
            {
                switch (component)
                {
                    case Cpu cpu:
                        _root.Cpu.Remove(cpu);
                        break;
                    case CpuCooler cpuCooler:
                        _root.CpuCooler.Remove(cpuCooler);
                        break;
                    case Gpu gpu:
                        _root.Gpu.Remove(gpu);
                        break;
                    case Motherboard motherboard:
                        _root.Motherboard.Remove(motherboard);
                        break;
                    case Memory memory:
                        _root.Memory.Remove(memory);
                        break;
                    case Ssd ssd:
                        _root.Ssd.Remove(ssd);
                        break;
                    case Hdd hdd:
                        _root.Hdd.Remove(hdd);
                        break;
                    case Psu psu:
                        _root.Psu.Remove(psu);
                        break;
                    case Case case_:
                        _root.Case.Remove(case_);
                        break;
                    case CaseFan caseFan:
                        _root.Case_Fan.Remove(caseFan);
                        break;
                    case Os os:
                        _root.Os.Remove(os);
                        break;
                }
            }); 
        }
        public void ClearComponents()
        {
            foreach (var property in _root.GetType().GetProperties())
            {
                property.SetValue(_root, Activator.CreateInstance(property.PropertyType));
            }
        }
    }
}

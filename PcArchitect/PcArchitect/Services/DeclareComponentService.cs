using PcArchitect.Model;

namespace PcArchitect.Services
{
    public class DeclareComponentService
    {
        public Task DeclareComponentTypeAsync(string component, object collectedPart)
        {
            return Task.Run(() =>
            {
                Root root = new Root();//!!!!!!!!!!!!!
                switch (component.ToLower())
                {
                    case "cpu":
                        root.Cpu.Add((Cpu)collectedPart);
                        break;
                    case "cpu cooler":
                        root.CpuCooler.Add((CpuCooler)collectedPart);
                        break;
                    case "motherboard":
                        root.Motherboard.Add((Motherboard)collectedPart);
                        break;
                    case "memory":
                        root.Memory.Add((Memory)collectedPart);
                        break;
                    case "gpu":
                        root.Gpu.Add((Gpu)collectedPart);
                        break;
                    case "ssd":
                        root.InternalStorage.Add((Ssd)collectedPart);
                        break;
                    case "hdd":
                        root.ExternalStorage.Add((Hdd)collectedPart);
                        break;
                    case "psu":
                        root.Psu.Add((Psu)collectedPart);
                        break;
                    case "case":
                        root.Case.Add((Case)collectedPart);
                        break;
                    case "case fan":
                        root.Case_Fan.Add((CaseFan)collectedPart);
                        break;
                    case "os":
                        root.Os.Add((Os)collectedPart);
                        break;
                }
            });
        }
    }
}

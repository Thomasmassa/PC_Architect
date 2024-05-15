using PcArchitect.Model;

/*
Deze klasse wordt gebruikt om componenten toe te voegen, te verwijderen en te verwijderen uit de Root klasse.

Het readonly sleutelwoord wordt gebruikt om ervoor te zorgen dat de waarde van de _rootF variabele alleen kan worden ingesteld in de constructor of bij de declaratie.
Dit zorgt ervoor dat de waarde van de variabele niet per ongeluk kan worden gewijzigd.

De RootFactory klasse wordt geïnjecteerd via de constructor (een techniek genaamd "Constructor Injection"). 
Deze klasse biedt toegang tot de Root klasse en zijn methoden.

De AddComponentAsync<T>(T component) methode voegt een component van een bepaald type toe aan de bijbehorende lijst in de Root klasse. 
Het gebruikt een switch statement om het type van het component te bepalen en voegt het vervolgens toe aan de juiste lijst.

De RemoveComponentAsync<T>(T component) methode werkt op een vergelijkbare manier, 
maar in plaats van een component toe te voegen, verwijdert het een component uit de bijbehorende lijst in de Root klasse.

De ClearComponents() methode maakt alle lijsten in de Root klasse leeg. 
Het doet dit door over alle eigenschappen van de Root klasse te itereren en voor elke eigenschap een nieuwe instantie van het eigenschapstype te maken. 
Dit effectief maakt de lijst leeg.

In `AddComponentAsync`, wordt de taak gecreëerd met `Task.Run` en vervolgens onmiddellijk `Task.CompletedTask` geretourneerd. 
Dit betekent dat de methode niet wacht tot de taak is voltooid voordat deze terugkeert.
In `RemoveComponentAsync`, wordt `await` gebruikt met `Task.Run`, wat betekent dat de methode zal wachten tot de taak die door `Task.Run` wordt gemaakt, 
is voltooid voordat het verder gaat.
*/

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
                    case Storage storage:
                        _rootF.GetRoot2().Storage.Add(storage);
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
                    case Storage storage:
                        _rootF.GetRoot2().Storage.Remove(storage);
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

        public Task ClearComponents()
        {
            return Task.Run(() =>
            {
                foreach (var property in _rootF.GetRoot2().GetType().GetProperties())
                    property.SetValue(_rootF.GetRoot2(), Activator.CreateInstance(property.PropertyType));

            });
        }
    }
}

using SQLite;

/*
SQLite is een database engine die wordt gebruikt om de gegevens van de applicatie op te slaan.
De attributen (zoals PrimaryKey) worden gebruikt om de eigenschappen van de klasse te markeren als kolommen in de database.

De klasse SavedBuild bevat de eigenschappen van een opgeslagen build.
Deze klasse wordt gebruikt om de gegevens van een build op te slaan in de database.
De eigenschappen van de klasse zijn de naam van de build en de ID's van de onderdelen van de build.

De eigenschappen van de klasse zijn gemarkeerd met de attributen PrimaryKey en Ignore
om aan te geven dat de eigenschap de primaire sleutel is en dat de eigenschap niet moet worden opgeslagen in de database.
De eigenschappen van de klasse hebben een getter en setter die de ID's van de onderdelen omzetten van en naar een string.

SQLite ondersteunt geen lijsten als gegevenstypen, dus de ID's van de onderdelen worden opgeslagen als een string van komma-gescheiden waarden.
*/

namespace PcArchitect.Model
{
    public class SavedBuild
    {
        [PrimaryKey]
        public string BuildName { get; set; } = "";


        [Ignore]
        public List<int> Cpu { get; set; } = [];
        public string CpuString
        {
            get => string.Join(",", Cpu);
            set => Cpu = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> CpuCooler { get; set; } = [];
        public string CpuCoolerString
        {
            get => string.Join(",", CpuCooler);
            set => CpuCooler = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Motherboard { get; set; } = [];
        public string MotherboardString
        {
            get => string.Join(",", Motherboard);
            set => Motherboard = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Memory { get; set; } = [];
        public string MemoryString
        {
            get => string.Join(",", Memory);
            set => Memory = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Gpu { get; set; } = [];
        public string GpuIdString
        {
            get => string.Join(",", Gpu);
            set => Gpu = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Storage { get; set; } = [];
        public string StorageString
        {
            get => string.Join(",", Storage);
            set => Storage = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Psu { get; set; } = [];
        public string PsuIdString
        {
            get => string.Join(",", Psu);
            set => Psu = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Case { get; set; } = [];
        public string CaseString
        {
            get => string.Join(",", Case);
            set => Case = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> CaseFan { get; set; } = [];
        public string CaseFanString
        {
            get => string.Join(",", CaseFan);
            set => CaseFan = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> Os { get; set; } = [];
        public string OsString
        {
            get => string.Join(",", Os);
            set => Os = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }
    }
}

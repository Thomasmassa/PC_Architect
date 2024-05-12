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
        public List<int> CpuId { get; set; } = [];
        public string CpuIdString
        {
            get => string.Join(",", CpuId);
            set => CpuId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> CpuCoolerId { get; set; } = [];
        public string CpuCoolerIdString
        {
            get => string.Join(",", CpuCoolerId);
            set => CpuCoolerId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> MotherboardId { get; set; } = [];
        public string MotherboardIdString
        {
            get => string.Join(",", MotherboardId);
            set => MotherboardId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> MemoryId { get; set; } = [];
        public string MemoryIdString
        {
            get => string.Join(",", MemoryId);
            set => MemoryId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> GpuId { get; set; } = [];
        public string GpuIdString
        {
            get => string.Join(",", GpuId);
            set => GpuId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> StorageId { get; set; } = [];
        public string StorageIdString
        {
            get => string.Join(",", StorageId);
            set => StorageId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> PsuId { get; set; } = [];
        public string PsuIdString
        {
            get => string.Join(",", PsuId);
            set => PsuId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> CaseId { get; set; } = [];
        public string CaseIdString
        {
            get => string.Join(",", CaseId);
            set => CaseId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> CaseFanId { get; set; } = [];
        public string CaseFanIdString
        {
            get => string.Join(",", CaseFanId);
            set => CaseFanId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }

        [Ignore]
        public List<int> OsId { get; set; } = [];
        public string OsIdString
        {
            get => string.Join(",", OsId);
            set => OsId = string.IsNullOrEmpty(value) ? new List<int> { 0 } : value.Split(',').Select(int.Parse).ToList();
        }
    }
}

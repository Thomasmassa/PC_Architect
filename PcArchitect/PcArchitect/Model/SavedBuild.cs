using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

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

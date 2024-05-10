using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcArchitect.Model
{
    public class SavedBuild
    {
        [PrimaryKey]
        public string BuildName { get; set; } = "";

        public int CpuId { get; set; }
        public int CpuCoolerId { get; set; }
        public int MotherboardId { get; set; }
        public int GpuId { get; set; } 
        public int MemoryId { get; set; }
        public int StorageId { get; set; }
        public int PsuId { get; set; }
        public int CaseId { get; set; }
        public int CaseFanId { get; set; }
        public int OsId { get; set; }
    }
}

namespace PC_ArchitectInstaller.Models
{
    public class Motherboard
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Socket { get; set; }
        public string Form_factor { get; set; }
        public int Max_memory { get; set; }
        public int Memory_slots { get; set; }
        public string Color { get; set; } = "";

    }
}
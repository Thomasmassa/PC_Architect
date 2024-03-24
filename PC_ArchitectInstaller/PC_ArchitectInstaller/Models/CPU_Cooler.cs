namespace PC_ArchitectInstaller.Models
{
    public class CPU_Cooler
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public string Color { get; set; } = "";
        public int[] Rpm { get; set; }
        public int[] Airflow { get; set; }
        public int[] Noise_level { get; set; }
        public bool Pwm { get; set; }
    }
}
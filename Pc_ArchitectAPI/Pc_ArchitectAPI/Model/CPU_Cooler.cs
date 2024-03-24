using System.ComponentModel.DataAnnotations;

namespace PC_ArchitectInstaller.Models
{
    public class CPU_Cooler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public string Color { get; set; } = "";
        public string Rpm { get; set; } = "";
        public string Airflow { get; set; } = "";
        public string Noise_level { get; set; } = "";
        public bool Pwm { get; set; }
    }
}
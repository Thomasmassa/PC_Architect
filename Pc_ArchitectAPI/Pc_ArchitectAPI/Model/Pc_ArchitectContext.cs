using Microsoft.EntityFrameworkCore;

namespace PC_ArchitectInstaller.Models
{
    public class Pc_ArchitectContext : DbContext
    {
        public Pc_ArchitectContext(DbContextOptions<Pc_ArchitectContext> options) : base(options)
        {

        }
        public DbSet<CPU_Cooler> cpu_coolers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CPU_Cooler>().HasData(
                new CPU_Cooler
                {
                    Id = 1,
                    Name = "Cooler Master Hyper 212 RGB",
                    Price = 44.99,
                    Size = 120,
                    Color = "RGB",
                    Rpm = new int[] { 650, 2000 },
                    Airflow = new int[] { 57, 65 },
                    Noise_level = new int[] { 8, 30 },
                    Pwm = true
                }
            );
        }
    }
}

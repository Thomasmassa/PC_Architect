using CategoryTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryTest.Service
{
    public class MockCategorieService : ICategorieService
    {
        public async Task<List<Category>?> GetAll()
        {
            return await Task.FromResult(new List<Category>
            {
                new Category { Id = 1, Name = "CPU" , Image = "cpu.png"},
                new Category { Id = 2, Name = "CPU Cooler" , Image = "cpu_cooler.png"},
                new Category { Id = 3, Name = "Motherboard" , Image = "motherboard.png" },
                new Category { Id = 4, Name = "Memory" , Image = "memory.png" },
                new Category { Id = 5, Name = "Graphics Card" , Image = "graphic_card.png" },
                new Category { Id = 6, Name = "Storage" , Image = "ssd.png" },
                new Category { Id = 7, Name = "Power Supply" , Image = "psu.png" },
                new Category { Id = 8, Name = "Case" , Image = "pc_tower.png" },
                new Category { Id = 9, Name = "Case Fan" , Image = "fan.png" }
            });
        }
    }
}

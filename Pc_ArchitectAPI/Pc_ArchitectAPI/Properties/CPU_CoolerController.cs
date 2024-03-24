using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_ArchitectInstaller.Models;

namespace Pc_ArchitectAPI.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPU_CoolerController : ControllerBase
    {
        private readonly Pc_ArchitectContext _context;

        public CPU_CoolerController(Pc_ArchitectContext context)
        {
            _context = context;
        }

        // GET: api/CPU_Cooler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPU_Cooler>>> Getcpu_coolers()
        {
            return await _context.cpu_coolers.ToListAsync();
        }

        // GET: api/CPU_Cooler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CPU_Cooler>> GetCPU_Cooler(int id)
        {
            var cPU_Cooler = await _context.cpu_coolers.FindAsync(id);

            if (cPU_Cooler == null)
            {
                return NotFound();
            }

            return cPU_Cooler;
        }

        // PUT: api/CPU_Cooler/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCPU_Cooler(int id, CPU_Cooler cPU_Cooler)
        {
            if (id != cPU_Cooler.Id)
            {
                return BadRequest();
            }

            _context.Entry(cPU_Cooler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CPU_CoolerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CPU_Cooler
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CPU_Cooler>> PostCPU_Cooler(CPU_Cooler cPU_Cooler)
        {
            _context.cpu_coolers.Add(cPU_Cooler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCPU_Cooler", new { id = cPU_Cooler.Id }, cPU_Cooler);
        }

        // DELETE: api/CPU_Cooler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCPU_Cooler(int id)
        {
            var cPU_Cooler = await _context.cpu_coolers.FindAsync(id);
            if (cPU_Cooler == null)
            {
                return NotFound();
            }

            _context.cpu_coolers.Remove(cPU_Cooler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CPU_CoolerExists(int id)
        {
            return _context.cpu_coolers.Any(e => e.Id == id);
        }
    }
}

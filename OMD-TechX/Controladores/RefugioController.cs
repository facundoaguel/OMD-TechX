using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;
using OMD_TechX.Pages;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/refugios")]
    public class RefugioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RefugioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<List<Refugio>>> Get()
        {
            return await context.Refugios.ToListAsync();
        }

        

        [HttpGet("{id}", Name = "getRefugio")]
        public async Task<ActionResult<Refugio>> Get(int id)
        {
            return await context.Refugios.FirstOrDefaultAsync(r => r.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Refugio refugio)
        {
            context.Add(refugio);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getRefugio", new { Id = refugio.Id }, refugio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var refugio = await context.Refugios.FindAsync(id);
            if (refugio == null)
            {
                return NotFound();
            }
            context.Refugios.Remove(refugio);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Refugio refugio)
        {
            context.Entry(refugio).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
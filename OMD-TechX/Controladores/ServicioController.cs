using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;
using System.Linq.Dynamic.Core;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/servicios")]
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ServicioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Servicio>>> Get()
        {
            return await context.Servicios.ToListAsync();
        }

        [HttpGet("paseadores")]
        public async Task<ActionResult<List<Servicio>>> GetPaseadores()
        {
            return await context.Servicios.Where(s => s.Tipo.Equals("Paseador")).ToListAsync();
        }

        [HttpGet("cuidadores")]
        public async Task<ActionResult<List<Servicio>>> GetCuidadores()
        {
            return await context.Servicios.Where(s => s.Tipo.Equals("Cuidador")).ToListAsync();
        }

        [HttpGet("{id}", Name = "getServicio")]
        public async Task<ActionResult<Servicio>> Get(int id)
        {
            return await context.Servicios.FirstOrDefaultAsync(a => a.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Servicio servicio)
        {
            context.Add(servicio);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getServicio", new { Id = servicio.Id }, servicio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servicio = await context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            context.Servicios.Remove(servicio);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Servicio servicio)
        {
            context.Entry(servicio).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
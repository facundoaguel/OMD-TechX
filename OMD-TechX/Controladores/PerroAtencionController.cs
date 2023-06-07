using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/perroAtencion")]
    public class PerroAtencionController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public PerroAtencionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PerroAtencion>>> Get()
        {
            return await context.PerroAtenciones.ToListAsync();
        }
        [HttpGet("{perroId}", Name = "getPerroAtencion")]
        public async Task<ActionResult<List<PerroAtencion>>> Get(int id)
        {
            return await context.PerroAtenciones.Where(pa => pa.PerroId == id).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(PerroAtencion perroAtencion)
        {
            var check = chequearAtencion(perroAtencion);
            if (!check)
            {
                context.Add(perroAtencion);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("getPerroAtencion", new { Id = perroAtencion.PerroId }, perroAtencion);
            }
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var perroAtencion = await context.PerroAtenciones.FindAsync(id);
            if (perroAtencion == null)
            {
                return NotFound();
            }
            context.PerroAtenciones.Remove(perroAtencion);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(PerroAtencion perroAtencion)
        {
            context.Entry(perroAtencion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        public bool chequearAtencion(PerroAtencion perroAtencion)
        {
            return context.PerroAtenciones.Any(pa => pa.AtencionId == perroAtencion.AtencionId && pa.Fecha == perroAtencion.Fecha && pa.PerroId == perroAtencion.PerroId);
        }
    }
}

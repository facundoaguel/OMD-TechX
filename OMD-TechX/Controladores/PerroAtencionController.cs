using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("/api/perroAtencion")]
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

        [HttpGet("{id}", Name = "getPerroAtencion")]
        public async Task<ActionResult<PerroAtencion>> Get(int id)
        {
            return await context.PerroAtenciones.FirstOrDefaultAsync(pa => pa.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(PerroAtencion perroAtencion)
        {
            context.Add(perroAtencion);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getPerroAtencion", new { Id = perroAtencion.Id }, perroAtencion);
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
    }
}

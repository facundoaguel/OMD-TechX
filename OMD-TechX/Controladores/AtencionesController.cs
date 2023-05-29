using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/atenciones")]
    public class AtencionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AtencionesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Atencion>>> Get()
        {
            return await context.Atenciones.ToListAsync();
        }
        [HttpGet("{id}", Name = "getAtencion")]
        public async Task<ActionResult<Atencion>> Get(int id)
        {
            return await context.Atenciones.FirstOrDefaultAsync(a => a.Id == id);
        }

        [HttpGet("getNombre/{nombre}")]
        public async Task<ActionResult<bool>> Get(string nombre)
        {
            return context.Atenciones.Any(a => a.Nombre.Equals(nombre));
        }

        [HttpPost]
        public async Task<ActionResult> Post(Atencion atencion)
        {
            context.Add(atencion);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getAtencion", new { Id = atencion.Id}, atencion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var atencion = await context.Atenciones.FindAsync(id);
            if (atencion == null)
            {
                return NotFound();
            }
            context.Atenciones.Remove(atencion);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Atencion atencion)
        {
            context.Entry(atencion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

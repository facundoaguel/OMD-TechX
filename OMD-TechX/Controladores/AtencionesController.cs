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

        [HttpPost]
        public async Task<ActionResult> Post(Atencion atencion)
        {
            context.Add(atencion);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("https://localhost:7083/api/get-atencion", new { Id = atencion.Id, Nombre = atencion.Nombre });
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
    }
}

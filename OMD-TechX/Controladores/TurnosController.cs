using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/turnos")]
    public class TurnosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TurnosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Turno>>> Get()
        {
            return await context.Turnos.ToListAsync();
        }
        [HttpGet("{id}", Name = "getTurno")]
        public async Task<ActionResult<Turno>> Get(int id)
        {
            return await context.Turnos.FirstOrDefaultAsync(a => a.Id == id);
        }

        /* [HttpGet("getNombre/{nombre}")]
         public async Task<ActionResult<bool>> Get(string nombre)
         {
             return context.Atenciones.Any(a => a.Nombre.Equals(nombre));
         }*/

        [HttpPost]
        public async Task<ActionResult> Post(Turno turno)
        {
            context.Add(turno);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getTurno", new { Id = turno.Id }, turno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var turno = await context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            context.Turnos.Remove(turno);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Turno turno)
        {
            context.Entry(turno).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
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
        [HttpGet("pendientes")]
        public async Task<ActionResult<List<Turno>>> GetPendientes()
        {
            return await context.Turnos.Where(t => t.estado.Equals("Pendiente") && t.Fecha.Date >= DateTime.Now.Date).ToListAsync();
        }
        [HttpGet("aceptados")]
        public async Task<ActionResult<List<Turno>>> GetAceptados()
        {
            return await context.Turnos.Where(t => t.estado.Equals("Aceptado")).ToListAsync();
        }

        [HttpGet("cancelados")]
        public async Task<ActionResult<List<Turno>>> GetCancelados()
        {
            return await context.Turnos.Where(t => t.estado.Equals("Rechazado")).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Turno>>> Get()
        {
            return await context.Turnos.ToListAsync();
        }
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<List<Turno>>> Get(string userId)
        {
            if (userId != null)
            {
                return await context.Turnos.Where(t => t.usuarioId == userId && ((t.Fecha.Date >= DateTime.Now.Date && t.estado.Equals("Pendiente"))||(!t.estado.Equals("Pendiente"))) ).ToListAsync();
            }
            else
            {
                return new List<Turno>();
            }
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
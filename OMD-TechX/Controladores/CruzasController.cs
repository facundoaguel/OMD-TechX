using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/cruzas")]
    public class CruzasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CruzasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cruza>>> Get()
        {
            return await context.Cruzas.ToListAsync();
        }

        [HttpGet("byUser/{id}")]
        public async Task<ActionResult<List<Cruza>>> GetByUserId(string id)
        {
            return await context.Cruzas.Where(c => c.UsuarioId == id).ToListAsync();
        }
        [HttpGet("byPerro/{id}")]
        public async Task<ActionResult<List<Cruza>>> GetPerroId(int id)
        {
            return await context.Cruzas.Where(c => c.PerroId == id).ToListAsync();
        }

        [HttpGet("{id}", Name = "getCruza")]
        public async Task<ActionResult<Cruza>> Get(int id)
        {
            return await context.Cruzas.FirstOrDefaultAsync(c => c.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cruza cruza)
        {
            context.Add(cruza);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getCruza", new { id = cruza.Id }, cruza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            context.Cruzas.Remove(await context.Cruzas.FirstOrDefaultAsync(c => c.Id == id));

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> Put(Cruza cruza)
        {
            context.Entry(cruza).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

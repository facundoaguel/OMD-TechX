using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/perros")]
    
    public class PerrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PerrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Perro>>> Get()
        {
            return await context.Perros.ToListAsync();

        }
        [HttpGet("{id}", Name = "getPerro")]
        public async Task<ActionResult<Perro>> Get(int id)
        {
            return await context.Perros.FirstOrDefaultAsync(p => p.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Perro perro)
        {
            context.Add(perro);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getPerro", new { id = perro.Id }, perro); ;

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            context.Perros.Remove(await context.Perros.FirstOrDefaultAsync(p => p.Id == id));

            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Perro perro)
        {
            context.Entry(perro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}

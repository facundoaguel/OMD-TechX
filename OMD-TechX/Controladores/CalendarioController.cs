using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/calendario")]
    public class CalendarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CalendarioController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Calendario>> Get()
        {
            return await context.Calendario.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Calendario calendario)
        {
            context.Add(calendario);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Calendario calendario)
        {
            context.Entry(calendario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calendario = await context.Calendario.FindAsync(id);
            if (calendario == null)
            {
                return NotFound();
            }
            context.Calendario.Remove(calendario);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

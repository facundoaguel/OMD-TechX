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
            return await context.Perros.Where(p => p.eliminado == false).Include(p => p.Turnos).Include(p => p.PerroAtencion).ToListAsync();

        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<Perro>>> GetTodos()
        {
            return await context.Perros.ToListAsync();
        }

        [HttpGet("{id}", Name = "getPerro")]
        public async Task<ActionResult<Perro>> Get(int id)
        {
            return await context.Perros.Where(p=> p.eliminado == false).Include(p => p.Turnos).Include(p => p.PerroAtencion).FirstOrDefaultAsync(p => p.Id == id);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<List<Perro>>> Get(string userId)
        {
            if (userId != null)
            {
                return await context.Perros.Where(p => p.UsuarioId == userId && p.eliminado == false).Include(p => p.Turnos).ToListAsync();
            }
            else
            {
                return new List<Perro>();
            }
        }

        [HttpGet("actuales")]
        public async Task<ActionResult<List<Perro>>> GetPerrosActuales()
        {
            return await context.Perros.Include(p => p.Turnos).Where(p => p.eliminado == false).ToListAsync();
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

        [HttpPut("eliminado")]
        public async Task<ActionResult> borradoLogico(Perro perro)
        {
            perro.eliminado = true;
            context.Entry(perro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Perro perro)
        {
            var existingPerro = await context.Perros.FirstOrDefaultAsync(p => p.Id == perro.Id);

            if (existingPerro != null)
            {
                // Actualizar las propiedades del perro existente con los valores del objeto perro
                existingPerro.Nombre = perro.Nombre;
                existingPerro.FechaN = perro.FechaN;
                existingPerro.Foto = perro.Foto;
                existingPerro.Raza = perro.Raza;
                existingPerro.Tamanio = perro.Tamanio;
                existingPerro.Color = perro.Color;
                existingPerro.UsuarioId = perro.UsuarioId;
                existingPerro.Comentarios = perro.Comentarios;
                existingPerro.Sexo = perro.Sexo;
                existingPerro.eliminado = perro.eliminado;
                //context.Entry(perro).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                // La entidad no existe en el contexto
                return NotFound();
            }
        }

    }
}

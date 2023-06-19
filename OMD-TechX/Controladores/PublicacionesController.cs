using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/publicaciones")]
    public class PublicacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PublicacionesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("adopciones")]
        public async Task<ActionResult<List<Adopcion>>> Get()
        {
            return await context.Publicaciones.OfType<Adopcion>().ToListAsync();     
        }

        //get adopciones by userId
        [HttpGet("adopciones/{id}")]
        public async Task<ActionResult<List<Adopcion>>> GetAdopcionesByUserId(string id)
        {
            return await context.Publicaciones.OfType<Adopcion>().Where(a => a.UsuarioId == id && a.completado == false).ToListAsync();
        }
       

        [HttpGet("{id}", Name = "getPublicacion")]
        public async Task<ActionResult<Publicacion>> Get(int id)
        {
            return await context.Publicaciones.FirstOrDefaultAsync(p => p.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Adopcion publicacion)
        {
            context.Add(publicacion);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getPublicacion", new { id = publicacion.Id }, publicacion); ;

        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            context.Publicaciones.Remove(await context.Publicaciones.FirstOrDefaultAsync(p => p.Id == id));

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Publicacion publicacion)
        {
            context.Entry(publicacion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

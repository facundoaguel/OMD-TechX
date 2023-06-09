using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using OMD_TechX.Data;
using OMD_TechX.Helpers;
using OMD_TechX.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OMD_TechX.Controladores
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}", Name = "getUsuario")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            return await context.Usuarios.Include(u => u.Perros).FirstOrDefaultAsync(u => u.Id == id);
        }

        //las peticiones GET a /api/users van a responder a este metodo
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.Include(u => u.Perros).ToListAsync();
        }

        [HttpGet("actuales")]
        public async Task<ActionResult<List<Usuario>>> GetUsuariosActuales()
        {
            return await context.Usuarios.Include(u => u.Perros).Where(u => u.eliminado == false).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Usuario user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getUsuario", new { id = user.Id }, user);
        }
        [HttpPut]
        public async Task<ActionResult> Put(Usuario user)
        {
            user.primerInicio = false;
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("eliminado")]
        public async Task<ActionResult> borradoLogico(Usuario user)
        {
            user.eliminado = true;
            context.Entry(user).State = EntityState.Modified;
            context.Users.Remove(await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id));
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("modificacion")]
        public async Task<ActionResult> PutNormal(Usuario user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            
            context.Usuarios.Remove(await context.Usuarios.FirstOrDefaultAsync(u=> u.Id == id));
            context.Users.Remove(await context.Users.FirstOrDefaultAsync(u => u.Id == id));

            await context.SaveChangesAsync();

            return NoContent();
        }
        public bool VerificarDNI(string dni)
        {
            return context.Usuarios.Any(u => u.DNI == dni);
        }
        
    }
}

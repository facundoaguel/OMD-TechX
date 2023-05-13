using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        //las peticiones GET a /api/users van a responder a este metodo
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            //me guardo en una variable si el GET fue llamado desde UserSection
            // (ya que ese header solo lo agregue desde esa parte)
            var isAdmin = Request.Headers["ADMIN"] == "true";

            //me fijo desde donde fue llamado si desde la seccion de usuarios
            //o desde la URL /api/users
            if (isAdmin || ((User.Identity.Name != null) && User.Identity.Name.Equals("pedro@omd.com")))
            {
                //devuelvo la lista de usuarios sacada del context de la DB
                return await context.Usuarios.ToListAsync();
            }
            else
            {
                return this.Redirect("/");
            }

            //lista creada para probar si funcionaba la llamada GET
            /*return new List<User>()
            {
                 new User(){Id="1", Name="Facundo"},
                 new User(){Id="2", Name="Juana"}
            };*/
        }

        /*[HttpGet("{id}", Name = "https://localhost:7083/api/get-user")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }*/


        [HttpPost]
        public async Task<ActionResult> Post(Usuario user)
        {
                context.Add(user);
                await context.SaveChangesAsync();
                return this.StatusCode(400);
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            context.Usuarios.Remove(usuario);

            await context.SaveChangesAsync(true);

            return NoContent();
        }
    }
}

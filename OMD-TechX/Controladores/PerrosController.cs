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
        [HttpPost]
        public async Task<ActionResult> Post(Perro perro)
        {
            context.Add(perro);
            context.SaveChangesAsync();
            return this.StatusCode(200);
        }
    }
}

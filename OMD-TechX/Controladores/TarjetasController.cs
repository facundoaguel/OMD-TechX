using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/tarjetas")]
    public class TarjetasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TarjetasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tarjeta>>> Get()
        {
            return await context.Tarjetas.ToListAsync();
        }

        [HttpGet("byNumero/{numero}")]
        public async Task<ActionResult<Tarjeta>> GetPorNumero(string numero)
        {
            return await context.Tarjetas.FirstOrDefaultAsync(t => t.Numero.Equals(numero));
        }
    }
}

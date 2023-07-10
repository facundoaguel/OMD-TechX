using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;

namespace OMD_TechX.Controladores
{
    [ApiController]
    [Route("api/donaciones")]

    public class DonacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DonacionesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Donacion>>> Get()
        {
            return await context.Donaciones.ToListAsync();

        }
        [HttpGet("{id}", Name = "getDonacion")]
        public async Task<ActionResult<Donacion>> Get(int id)
        {
            return await context.Donaciones.FirstOrDefaultAsync(p => p.Id == id);
        }

        [HttpGet("byRefugio/{refugioId}")]
        public async Task<ActionResult<List<Donacion>>> GetByRefugio(int refugioId)
        {
            if (refugioId != null)
            {
                return await context.Donaciones.Where(p => p.RefugioId == refugioId).ToListAsync();
            }
            else
            {
                return new List<Donacion>();
            }
        }

       

        [HttpPost]
        public async Task<ActionResult> Post(Donacion donacion)
        {
            context.Add(donacion);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("getDonacion", new { id = donacion.Id }, donacion); ;

        }
        

        


    }
}
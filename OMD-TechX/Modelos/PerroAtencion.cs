using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class PerroAtencion
    {
        [Key]
        public int Id { get; set; }
        public int PerroId { get; set; }
        public Perro perro { get; set; }
        public Atencion Atencion { get; set; }
        public int AtencionId { get; set; }
        public DateTime Fecha { get; set; }
    }
}

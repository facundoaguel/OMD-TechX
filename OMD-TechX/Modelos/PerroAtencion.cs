using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class PerroAtencion
    {
        [Key]
        public string Id { get; set; }
        public string PerroId { get; set; }
        public Perro perro { get; set; }
        public Atencion Atencion { get; set; }
        public string AtencionId { get; set; }
        public DateTime Fecha { get; set; }
    }
}

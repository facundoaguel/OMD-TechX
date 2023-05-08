using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Disponibilidad
    {
        [Key]
        public string Id { get; set; }
        public int Cupos { get; set; }
        public string Franja { get; set; }
        public DateTime Fecha { get; set; }
    }
}

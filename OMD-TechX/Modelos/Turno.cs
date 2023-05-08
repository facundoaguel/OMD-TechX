using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Turno
    {
        [Key]
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Franja { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Atencion
    {
        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }

    }
}

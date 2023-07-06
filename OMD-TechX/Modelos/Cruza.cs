using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Cruza
    {
        [Key]
        public int Id { get; set; }
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un perro.")]
        [Range(1, 35000, ErrorMessage = "Por favor, seleccione un perro")]
        public int PerroId { get; set; }

        public Cruza()
        {

        }
        public Cruza(string usuarioId, int perroId)
        {
            UsuarioId = usuarioId;
            PerroId = perroId;
        }
    }
}
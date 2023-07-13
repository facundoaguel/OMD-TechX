using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

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

        HttpClient http { get; set; } = new HttpClient();
        public Cruza()
        {

        }
        public Cruza(string usuarioId, int perroId)
        {
            UsuarioId = usuarioId;
            PerroId = perroId;
        }

        public async Task borrar()
        {
            await http.DeleteAsync($"https://localhost:7083/api/cruzas/{this.Id}");
        }
    }
}
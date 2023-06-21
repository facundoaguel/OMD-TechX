using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Publicacion
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "El campo dueño/a es requerido.")]
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Nombre admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo fecha es requerido.")]
        public DateTime FechaN { get; set; }
        [Required(ErrorMessage = "El campo raza es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Raza admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Raza { get; set; }
        [Required(ErrorMessage = "El campo tamaño es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Tamaño admite hasta 30 caracteres")]
        public string Tamanio { get; set; }
        [Required(ErrorMessage = "El campo sexo es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Sexo admite hasta 30 caracteres")]

        public string Sexo { get; set; }
        [Required(ErrorMessage = "El campo Color es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Color admite hasta 30 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Color { get; set; }
        //[StringLength(100, ErrorMessage = "El campo Color admite hasta 100 caracteres")]

        public bool completado { get; set; } = false;

        HttpClient http { get; set; } = new HttpClient();

        [JsonConstructor]
        public Publicacion(string nombre, DateTime fechaN, string raza, string tamanio, string sexo, string color, string usuarioId)
        {
            this.Nombre = nombre;
            this.FechaN = fechaN;
            this.Raza = raza;
            this.Tamanio = tamanio;
            this.Sexo = sexo;
            this.Color = color;
            this.UsuarioId = usuarioId;
        }
        public Publicacion() { }


        public async Task borrar()
        {
            await http.DeleteAsync($"https://localhost:7083/api/publicaciones/{this.Id}");
        }
    }
}

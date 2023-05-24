using OMD_TechX.Controladores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Perro
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "El campo dueño/a es requerido.")]

        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido.")]

        [PerroNombreUnico]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo fecha es requerido.")]
        public DateTime? FechaN { get; set; }
        [Required(ErrorMessage = "El campo raza es requerido.")]
        public string Raza { get; set; }
        [Required(ErrorMessage = "El campo tamaño es requerido.")]
        public string Tamanio { get; set; }
        [Required(ErrorMessage = "El campo sexo es requerido.")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "El campo color es requerido.")]
        public string Color { get; set; }
        public string? Comentarios { get; set; }
        public byte[]? Foto { get; set; }

        public List<Turno> Turnos {get; set; }
        public List<PerroAtencion> PerroAtencion { get; set; }
        public Perro(string nombre, DateTime fechaN, string raza,string tamanio, string sexo, string color, string com, string usuarioId)
        {
            this.Nombre = nombre; 
            this.FechaN = fechaN;
            this.Raza = raza;
            this.Tamanio = tamanio;
            this.Sexo = sexo;
            this.Color = color;
            this.Comentarios = com;
            this.UsuarioId = usuarioId;
            this.Turnos = new List<Turno>();
            this.PerroAtencion = new List<PerroAtencion>();

        }
        public Perro()
        {
            this.Turnos = new List<Turno>();
            this.PerroAtencion = new List<PerroAtencion>();
            this.Foto = new byte []{ };
        }


    }
}

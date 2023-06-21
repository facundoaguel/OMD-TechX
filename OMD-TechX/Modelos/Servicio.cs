using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Nombre admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Apellido admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo Email es requerido.")]
        [EmailAddress(ErrorMessage = "Este formato no es válido")]
        [StringLength(50, ErrorMessage = "El campo Email admite hasta 50 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Telefono es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Teléfono admite hasta 30 caracteres")]
        public string Telefono { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Franja { get; set; }



        public Servicio() { }
        public Servicio(string nombre, string apellido, string email, string telefono, string tipo, string franja)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            Tipo = tipo;
            Franja = franja;
        }
    }
}

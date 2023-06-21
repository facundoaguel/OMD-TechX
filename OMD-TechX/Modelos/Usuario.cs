using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Usuario
    {
        [Key]
        public string Id { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "El formato del mail no es valido.")]
        [Required(ErrorMessage ="El campo mail es requerido.")]
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool primerInicio { get; set; } = true;
        public bool eliminado { get; set; } = false;
        public List<Perro> Perros { get; set; } = new List<Perro>();

        public Usuario(string id, string nombre, string apellido, string DNI, string email, string telefono)
        {
            this.Id = id;
            this.Nombre = nombre; 
            this.Apellido = apellido;
            this.DNI = DNI;
            this.Email = email;
            this.Telefono = telefono;
        }

        public Usuario()
        {
            
        }
        public void eliminarPerro(Perro p)
        {
            this.Perros.Remove(p);
        }
    }
}

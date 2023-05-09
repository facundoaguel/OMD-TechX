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
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<Perro> Perros { get; set; }

        public Usuario(string id, string nombre, string apellido, string DNI, string email, string telefono)
        {
            this.Id = id;
            this.Nombre = nombre; 
            this.Apellido = apellido;
            this.DNI = DNI;
            this.Email = email;
            this.Telefono = telefono;
            this.Perros = new List<Perro>();
        }

        public Usuario()
        {
            
        }

    }
}

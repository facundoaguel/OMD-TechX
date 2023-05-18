using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Perro
    {

        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Raza { get; set; }
        public string Tamanio { get; set; }
        public string Sexo { get; set; }
        public string Color { get; set; }
        public string Comentarios { get; set; }
        public byte[]? Foto { get; set; }

        public List<Turno> Turnos {get; set; }
        public List<PerroAtencion> PerroAtencion { get; set; }  

        public Perro(string nombre, int edad, string raza,string tamanio, string sexo, string color, string com, string usuarioId)
        {
            this.Nombre = nombre; 
            this.Edad = edad;
            this.Raza = raza;
            this.Tamanio = tamanio;
            this.Sexo = sexo;
            this.Color = color;
            this.Comentarios = com;
            this.UsuarioId = usuarioId;
            this.Turnos = new List<Turno>();
            this.PerroAtencion = new List<PerroAtencion>();
            this.Foto = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            this.Usuario = new Usuario();
        }
        public Perro()
        {

        }


    }
}

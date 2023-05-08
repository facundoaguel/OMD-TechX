using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Perro
    {

        public string Id { get; set; }
        public Usuario Usuario { get; set; }
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public string Color { get; set; }
        public string Comentarios { get; set; }
        public byte[]? Foto { get; set; }

        public List<Turno> Turnos = new List<Turno>();
        public List<PerroAtencion> PerroAtencion = new List<PerroAtencion>();


    }
}

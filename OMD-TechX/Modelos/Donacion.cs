using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace OMD_TechX.Modelos
{
    public class Donacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public double Donado { get; set; }

        [Required]
        public int RefugioId { get; set; }

        public Donacion() { }   

        public Donacion(string Nombre, string Apellido, double Donado, int Id)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;   
            this.Donado = Donado;  
            this.RefugioId = Id;

        }
    }
}

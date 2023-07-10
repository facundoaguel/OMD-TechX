using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Refugio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [StringLength(22, ErrorMessage = "El CBU solo admite hasta 50 caracteres")]
        [RegularExpression(@"^[0-9\s]+$", ErrorMessage = "Ingrese solo numeros")]
        public string CBU { get; set; }

        [Required]
        public string Descripcion { get; set; }


        public Refugio() { }

        public Refugio(String Nombre, String Direccion, string CBU, String Descrpcion)
        {
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.CBU = CBU; 
            this.Descripcion = Descrpcion;
        }
    }
}

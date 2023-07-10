using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Refugio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo dirección es requerido")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo CBU es requerido")]
        [StringLength(22, ErrorMessage = "El CBU solo admite hasta 50 caracteres")]
        [RegularExpression(@"^[0-9\s]+$", ErrorMessage = "Ingrese solo numeros")]
        public string CBU { get; set; }

        [Required(ErrorMessage = "El campo descripción es requerido")]
        public string Descripcion { get; set; }


        public Refugio() { }

        public Refugio(string Nombre, string Direccion, string CBU, string Descrpcion)
        {
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.CBU = CBU; 
            this.Descripcion = Descrpcion;
        }
    }
}

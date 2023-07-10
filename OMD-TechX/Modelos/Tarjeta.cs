using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMD_TechX.Modelos
{
    public class Tarjeta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        [StringLength(100, ErrorMessage = "El campo Apellido admite hasta 100 caracteres")]
        [Display(Name = "Titular")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string NombreTitular { get; set; }

        [Required (ErrorMessage ="Debe ingresar el numero de la tarjeta")]
        [StringLength(maximumLength: 16, MinimumLength = 16, ErrorMessage = "El numero de tarjeta debe tener 16 digitos")]
        public string Numero { get; set;}
        [Required(ErrorMessage ="El codigo de seguridad es requerido")]
        public string CodigoDeSeguridad { get; set;}

        [Required(ErrorMessage = "El campo fecha de vencimiento es requerido")]
        public DateTime FechaVencimiento { get; set; }

        public double Monto { get;set; }

        public Tarjeta() { }

        public Tarjeta(string nombreTitular, string numero, string codigoDeSeguridad, DateTime fechaVencimiento, double monto)
        {
            NombreTitular = nombreTitular;
            Numero = numero;
            CodigoDeSeguridad = codigoDeSeguridad;
            FechaVencimiento = fechaVencimiento;
            Monto = monto;
        }
    }
}

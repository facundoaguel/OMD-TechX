using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMD_TechX.Modelos
{
    public class DonacionFormulario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        [StringLength(100, ErrorMessage = "El campo Apellido admite hasta 100 caracteres")]
        [Display(Name = "Titular")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string NombreTitular { get; set; }

        [Required(ErrorMessage = "Debe ingresar el numero de la tarjeta")]
        [StringLength(maximumLength: 16, MinimumLength = 16, ErrorMessage = "El numero de tarjeta debe tener 16 digitos")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "El codigo de seguridad es requerido")]
        [StringLength(maximumLength: 3, MinimumLength = 3, ErrorMessage = "El codigo de seguridad de la tarjeta debe tener 3 digitos")]
        public string CodigoDeSeguridad { get; set; }

        [Required(ErrorMessage = "El campo fecha de vencimiento es requerido")]
        public DateTime FechaVencimiento { get; set; }

        
        [Required(ErrorMessage = "debe ingresar un monto a donar")]
        public double MontoADonar { get; set; }

        public DonacionFormulario() { }

        public DonacionFormulario(string nombreTitular, string numero, string codigoDeSeguridad, DateTime fechaVencimiento, double montoADonar)
        {
            NombreTitular = nombreTitular;
            Numero = numero;
            CodigoDeSeguridad = codigoDeSeguridad;
            FechaVencimiento = fechaVencimiento;
            MontoADonar = montoADonar;
        }
    }
}

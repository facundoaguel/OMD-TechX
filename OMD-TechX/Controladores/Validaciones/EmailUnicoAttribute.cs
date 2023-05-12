using OMD_TechX.Data;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class EmailUnicoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Verificar si ya existe un usuario con este DNI en la base de datos
                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext)); // Reemplaza "TuDbContext" con el nombre de tu DbContext
                var user = dbContext.Usuarios.Any(u => u.Email == (string)value);

                if (user)
                {
                    return new ValidationResult($"El email {value} ya está en uso por otro usuario.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

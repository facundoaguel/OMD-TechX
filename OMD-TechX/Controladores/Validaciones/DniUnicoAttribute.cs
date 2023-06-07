using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class DniUnicoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Verificar si ya existe un usuario con este DNI en la base de datos
                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext)); // Reemplaza "TuDbContext" con el nombre de tu DbContext
                var user = dbContext.Usuarios.Where(u=>u.eliminado == false).FirstOrDefault(u => u.DNI == (string)value);

                if (user != null)
                {
                    return new ValidationResult("El DNI ya está en uso por otro usuario.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

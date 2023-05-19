using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class NombreUnicoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Verificar si ya existe un usuario con este DNI en la base de datos
                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext)); // Reemplaza "TuDbContext" con el nombre de tu DbContext
                var atencion = dbContext.Atenciones.FirstOrDefault(a => a.Nombre == (string)value);

                if (atencion != null)
                {
                    return new ValidationResult("El nombre ya está en uso.");
                }
            }

            return ValidationResult.Success;
        }
    }
}


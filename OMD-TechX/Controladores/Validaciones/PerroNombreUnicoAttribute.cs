using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class PerroNombreUnicoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<ApplicationDbContext>();
            var perro = validationContext.ObjectInstance as Perro;

            if (perro != null)
            {
                // Verificar si ya existe un perro con el mismo nombre para el mismo usuario
                var existingPerro = dbContext.Perros.FirstOrDefault(p => p.Nombre == perro.Nombre && p.UsuarioId == perro.UsuarioId);

                if (existingPerro != null)
                {
                    return new ValidationResult("El nombre del perro ya está en uso.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

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
            if (value != null)
            {
                // Verificar si ya existe un usuario con este DNI en la base de datos
                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext)); // Reemplaza "TuDbContext" con el nombre de tu DbContext
                var perro = dbContext.Perros.FirstOrDefault(p => p.Nombre == (string)value);

                Usuario usuario = dbContext.Usuarios.FirstOrDefault(u => u.Id == perro.UsuarioId);
                var encontre = false;

                if(usuario  != null)
                {
                    encontre = usuario.Perros.Any(p => p.Nombre.Equals(perro.Nombre));
                }
                if (encontre)
                {
                    return new ValidationResult("El nombre ya está en uso.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

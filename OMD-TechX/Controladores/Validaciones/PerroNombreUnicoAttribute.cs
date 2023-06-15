using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using OMD_TechX.Modelos;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class PerroNombreUnicoAttribute : ValidationAttribute
    {

        private readonly string usuarioId;

        public PerroNombreUnicoAttribute(string usuarioId)
        {
            this.usuarioId = usuarioId;
        }
        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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
        }*/

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<ApplicationDbContext>();
            var usuarioIdInfo = validationContext.ObjectType.GetProperty(usuarioId);
            if (usuarioIdInfo == null)
            {
                return new ValidationResult($"La propiedad {usuarioId} no fue encontrada.");
            }

            var usuarioIdValue = usuarioIdInfo.GetValue(validationContext.ObjectInstance, null);
            Usuario usuario = dbContext.Usuarios.Include(u => u.Perros).FirstOrDefault(u => u.Id == usuarioIdValue);
            if (usuario != null) {
                var encontre = usuario.Perros.Any(p => {Console.WriteLine("Nombre del perro: " + p.Nombre); return p.Nombre.Equals((string)value) && p.eliminado == false; });
                if (encontre)
                {
                    return new ValidationResult("El cliente ya tiene un perro asociado con ese nombre. Por favor, ingrese otro.");
                }
                else
                {
                    return ValidationResult.Success;
                }   
            }
            return ValidationResult.Success;
        }
    }
}

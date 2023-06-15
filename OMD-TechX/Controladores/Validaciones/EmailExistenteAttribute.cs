using Microsoft.EntityFrameworkCore;
using OMD_TechX.Data;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Controladores.Validaciones
{
    public class EmailExistenteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext)); // Reemplaza "TuDbContext" con el nombre de tu DbContext
                var user = dbContext.Usuarios.Where(u => u.eliminado == false).FirstOrDefault(u => u.Email == (string)value);

                if (user == null && !(value.Equals("pedro@omd.com")))
                {
                    return new ValidationResult("Email inexistente, por favor acerquese a la veterinaria para registrarse.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

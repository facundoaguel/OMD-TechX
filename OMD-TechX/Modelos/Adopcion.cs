using Newtonsoft.Json;
using System.Transactions;

namespace OMD_TechX.Modelos
{
    public class Adopcion : Publicacion
    {
        public string comentarios { get; set; }

        [JsonConstructor]
        public Adopcion(string nombre, DateTime fechaN, string raza, string tamanio, string sexo, string color, string usuarioId, string comentarios) : base(nombre, fechaN, raza, tamanio, sexo, color, usuarioId)
        {
            this.comentarios = comentarios;
        }
        public Adopcion() { }

        public int calcularEdad()
        {
            DateTime fechaActual = DateTime.Now; // Obtiene la fecha actual

            int edad = fechaActual.Year - this.FechaN.Year; // Calcula la diferencia en años

            // Verifica si el cumpleaños aún no ha ocurrido este año
            if (fechaActual.Month < this.FechaN.Month || (fechaActual.Month == this.FechaN.Month && fechaActual.Day < this.FechaN.Day))
            {
                edad--;
            }
            return edad;
        }
    }
}

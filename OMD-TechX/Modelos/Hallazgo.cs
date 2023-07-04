using Newtonsoft.Json;
using System.Transactions;

namespace OMD_TechX.Modelos
{
    public class Hallazgo : Publicacion
    {
        public string comentarios { get; set; }
        public byte[]? Foto { get; set; }

        [JsonConstructor]
        public Hallazgo(string nombre, DateTime fechaN, string raza, string tamanio, string sexo, string color, string usuarioId, string comentarios) : base(nombre, fechaN, raza, tamanio, sexo, color, usuarioId)
        {
            this.comentarios = comentarios;
            this.Foto = new byte[] { };
        }
        public Hallazgo()
        {
            this.Foto = new byte[] { };
        }

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


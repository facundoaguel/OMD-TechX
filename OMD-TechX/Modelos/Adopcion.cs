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

    }
}

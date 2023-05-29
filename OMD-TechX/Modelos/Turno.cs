using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMD_TechX.Modelos
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Franja { get; set; }
        public int PerroId { get; set; }
        public string Perro { get; set; }
        public string estado { get; set; }
        public int motivoId { get; set; }
        public string motivo { get; set; }
        public int usuarioId { get; set; }

        public Turno() { }

        public static async Task<string> ObtenerPerro(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7083/api/perros/{Id}");
                    response.EnsureSuccessStatusCode(); // Verificar que la respuesta sea exitosa
                    var perro = await response.Content.ReadFromJsonAsync<Perro>();
                    return perro.Nombre.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No hay perros cargados: {ex.Message}");
                    return null;
                }
            }
        }

        public static async Task<string> ObtenerMotivo(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7083/api/atenciones/{Id}");
                    response.EnsureSuccessStatusCode(); // Verificar que la respuesta sea exitosa
                    var atencion = await response.Content.ReadFromJsonAsync<Atencion>();
                    return atencion.Nombre.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No hay atenciones cargadas: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task EstablecerEstadoPorIdAsync(int perroId, int motivoId)
        {
            Perro = await ObtenerPerro(perroId);
            motivo = await ObtenerMotivo(motivoId);
        }

        public Turno(DateTime fecha, String franja, int perroId, int motivoId, int usuarioId)
        {
            this.usuarioId = usuarioId;
            this.Fecha = fecha;
            this.Franja = franja;
            this.estado = "Pendiente";
            this.PerroId = perroId;
            this.motivoId = motivoId;
            Task.Run(async () => await EstablecerEstadoPorIdAsync(perroId, motivoId)).Wait();
        }
    }

}

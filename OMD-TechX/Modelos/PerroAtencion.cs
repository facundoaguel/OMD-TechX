using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class PerroAtencion
    {
        [Key]
        public int Id { get; set; }
        public int PerroId { get; set; }
        public string perro { get; set; }
        public string Atencion { get; set; }
        public int AtencionId { get; set; }
        public DateTime Fecha { get; set; }

        public PerroAtencion(int perroId, int atencionId, DateTime fecha)
        {
            this.PerroId = perroId;
            this.AtencionId = atencionId;
            this.Fecha = fecha;
            Task.Run(async () => await EstablecerEstadoPorIdAsync(perroId, atencionId)).Wait();
        }
        public PerroAtencion()
        {

        }

        public async Task EstablecerEstadoPorIdAsync(int perroId, int atencionId)
        {
            this.perro = await ObtenerPerro(perroId);
            this.Atencion = await ObtenerAtencion(atencionId);
        }

        public static async Task<string> ObtenerPerro(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7083/api/perros/{Id}");
                    response.EnsureSuccessStatusCode(); // Verificar que la respuesta sea exitosa
                    var perro = await response.Content.ReadFromJsonAsync<Perro>();
                    return perro.Nombre;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No hay perros cargados: {ex.Message}");
                    return "";               }
            }
        }

        public static async Task<string> ObtenerAtencion(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7083/api/atenciones/{Id}");
                    response.EnsureSuccessStatusCode(); // Verificar que la respuesta sea exitosa
                    var atencion = await response.Content.ReadFromJsonAsync<Atencion>();
                    return atencion.Nombre;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No hay atenciones cargadas: {ex.Message}");
                    return "";
                }
            }
        }
    }

}

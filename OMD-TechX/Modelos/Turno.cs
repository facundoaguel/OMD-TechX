using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMD_TechX.Modelos
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        //[Required(ErrorMessage = "La hora es requerida")]
        public DateTime? hora { get; set; }
        [Required(ErrorMessage = "La franja horaria es requerida")]
        public string Franja { get; set; }
        [Required(ErrorMessage = "El campo perro es requerido")]
        [Range(1, 35000, ErrorMessage = "El campo perro es requerido")]
        public int PerroId { get; set; }
        public string Perro { get; set; }
        public string estado { get; set; }
        [Range(1, 35000, ErrorMessage = "El campo motivo es requerido")]
        [Required(ErrorMessage = "El campo motivo es requerido")]
        public int motivoId { get; set; }

        public string motivo { get; set; }
        public string usuarioId { get; set; }

        public Turno() {
            this.estado = "Pendiente";
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
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7083/api/atenciones/{Id}");
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
            this.Perro = await ObtenerPerro(perroId);
            this.motivo = await ObtenerMotivo(motivoId);
        }

        public Turno(DateTime fecha, String franja, int perroId, int motivoId, string usuarioId)
        {
            this.usuarioId = usuarioId;
            this.Fecha = fecha;
            this.Franja = franja;
            this.estado = "Pendiente";
            this.PerroId = perroId;
            this.motivoId = motivoId;
            this.hora = DateTime.Now;
            Task.Run(async () => await EstablecerEstadoPorIdAsync(perroId, motivoId)).Wait();
        }

        //en algun lugar tenemos que llamar este metodo,
        //podemos hacer que el veterinario tenga boton de actualizar libreta y entonces se llame a este metodo en todos los turno de ese perro
        public async void PasarAHistorial()
        {
            PerroAtencion nuevoHistorial = new PerroAtencion(this.PerroId, this.motivoId, this.Fecha);
                
            using (HttpClient client = new HttpClient())
            {
                Perro perro = await client.GetFromJsonAsync<Perro>($"https://localhost:7083/api/perros/{this.PerroId}");
                perro.PerroAtencion.Add(nuevoHistorial);
                var res = await client.PostAsJsonAsync("https://localhost:7083/api/perroAtencion", nuevoHistorial);
            }
        }
        public void setHora(DateTime hora)
        {
            this.hora = hora;
        }
    }

}

using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Atencion
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Atencion(string nombre, double precio)
        {
            this.Nombre = nombre;
            this.Precio = precio;
        }
        public Atencion() { }

        public Boolean EsOriginal()
        {
            return (this.Nombre.Equals("Vacuna Antirrabica") || this.Nombre.Equals("Operacion"));
        }
    }
}

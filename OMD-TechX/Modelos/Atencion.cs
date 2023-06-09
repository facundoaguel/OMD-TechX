﻿using OMD_TechX.Controladores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace OMD_TechX.Modelos
{
    public class Atencion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Nombre { get; set; }
        [Required (ErrorMessage ="El campo Precio es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser menor que 0")]
        public double Precio { get; set; }

        public Atencion(string nombre, double precio)
        {
            this.Nombre = nombre;
            this.Precio = precio;
        }
        public Atencion() { }

        public Boolean EsOriginal()
        {
            return (this.Nombre.Equals("Vacuna antirrabica") || this.Nombre.Equals("Castracion") || this.Nombre.Equals("Consulta general") || this.Nombre.Equals("Desparasitario") || this.Nombre.Equals("Vacuna por enfermedad"));
        }
    }
}

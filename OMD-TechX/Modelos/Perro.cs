using Microsoft.AspNetCore.Mvc;
using OMD_TechX.Controladores.Validaciones;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using Xunit.Sdk;

namespace OMD_TechX.Modelos
{
    public class Perro
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "El campo dueño/a es requerido.")]

        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido.")]

        [StringLength(50, ErrorMessage = "El campo Nombre admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo fecha es requerido.")]
        public DateTime FechaN { get; set; }
        [Required(ErrorMessage = "El campo raza es requerido.")]
        [StringLength(50, ErrorMessage = "El campo Raza admite hasta 50 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Raza { get; set; }
        [Required(ErrorMessage = "El campo tamaño es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Tamaño admite hasta 30 caracteres")]
        public string Tamanio { get; set; }
        [Required(ErrorMessage = "El campo sexo es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Sexo admite hasta 30 caracteres")]

        public string Sexo { get; set; }
        [Required(ErrorMessage = "El campo Color es requerido.")]
        [StringLength(30, ErrorMessage = "El campo Color admite hasta 30 caracteres")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Ingresa solo letras")]
        public string Color { get; set; }
        [StringLength(100, ErrorMessage = "El campo Color admite hasta 100 caracteres")]
        public string? Comentarios { get; set; }
        public byte[]? Foto { get; set; }

        public List<Turno> Turnos {get; set; }
        public List<PerroAtencion> PerroAtencion { get; set; }
        public bool eliminado { get; set; } = false;
        public Perro(string nombre, DateTime fechaN, string raza,string tamanio, string sexo, string color, string com, string usuarioId)
        {
            this.Nombre = nombre; 
            this.FechaN = fechaN;
            this.Raza = raza;
            this.Tamanio = tamanio;
            this.Sexo = sexo;
            this.Color = color;
            this.Comentarios = com;
            this.UsuarioId = usuarioId;
            this.Turnos = new List<Turno>();
            this.PerroAtencion = new List<PerroAtencion>();

        }
        public Perro()
        {
            this.Turnos = new List<Turno>();
            this.PerroAtencion = new List<PerroAtencion>();
            this.Foto = new byte []{ };
        }
    }
}

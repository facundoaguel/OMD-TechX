using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using OMD_TechX.Controladores.Validaciones;
using OMD_TechX.Helpers;
using OMD_TechX.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;

namespace OMD_TechX.Pages.Perros
{
    public class CargarPerroModel : PageModel
    {
        public HttpClient http;
        private readonly SignInManager<IdentityUser> _signInManager;
        public List<Usuario> usuarios;

        public CargarPerroModel(SignInManager<IdentityUser> signInManager)
        {
            this.http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:7083/");
            _signInManager = signInManager;
            this.usuarios = new List<Usuario>();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            //aca comienza la creacion

            //nombre
            [Required(ErrorMessage = "El campo Nombre es requerido.")]
            [StringLength(30)]
            public string Nombre { get; set; }

            //apellido
            [Required(ErrorMessage = "El campo Edad es requerido.")]
            public int Edad { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El campo Raza es requerido.")]
            [StringLength(30)]
            public string Raza { get; set; }
            [Required(ErrorMessage = "El campo Color es requerido.")]

            public string Color { get; set; }
            [Required]
            [StringLength(30)]
            public string Sexo { get; set; }
            [Required]
            [StringLength(30)]
            public string Tamanio { get; set; }
            [Required]            
            public string Comentarios { get; set; }
            [Required]
            public string UsuarioId { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            usuarios = await http.GetFromJsonAsync<List<Usuario>>("api/usuarios");
        }

        public async Task<IActionResult> OnPostAsync(string Normal = null)
        {
            Normal ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                string uId = Input.UsuarioId;
                Usuario usuario = await http.GetFromJsonAsync<Usuario>($"api/usuarios/{uId}");   
                await cargarPerro(Input.Nombre, Input.Edad, Input.Raza, Input.Tamanio, Input.Sexo,
                                    Input.Color, Input.Comentarios, Input.UsuarioId, usuario);
                return LocalRedirect("/perros");
            }
            return Page();
        }

        async Task cargarPerro(string nombre,int edad, string raza, string tamanio,
                                string sexo, string color, string coms, string uId, Usuario usuario)
        {
            Perro perro = new Perro(nombre, edad, raza, tamanio, sexo, color, coms, uId);
            HttpResponseMessage res = await http.PostAsJsonAsync("api/perros", perro);
            if (res.StatusCode == System.Net.HttpStatusCode.BadRequest || res.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            res.EnsureSuccessStatusCode();

            usuario.Perros.Add(perro);
            HttpResponseMessage res2 = await http.PutAsJsonAsync("api/usuarios", usuario);
            if (res2.StatusCode == System.Net.HttpStatusCode.BadRequest || res2.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var content2 = await res2.Content.ReadAsStringAsync();
                Console.WriteLine(content2);
            
            }
            res2.EnsureSuccessStatusCode();
        }
    }
}


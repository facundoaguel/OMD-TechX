using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OMD_TechX.Controladores.Validaciones;
using OMD_TechX.Modelos;
using System.ComponentModel.DataAnnotations;


namespace OMD_TechX.Areas.Identity.Pages.Account
{
    public class CrearAtencionModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public HttpClient http;
        public CrearAtencionModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:7083/");
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo Nombre es requerido.")]
            [StringLength(30)]
            [Display(Name = "Nombre")]
            [NombreUnico]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo Precio es requerido.")]
            [Display(Name = "Precio")]
            public double Precio { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string Normal = null)
        {
            Normal ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                crearAtencion(Input.Nombre, Input.Precio);
                return LocalRedirect("/atenciones");
            }
            return Page();
        }

        async Task crearAtencion(string nombre, double precio)
        {
            Atencion atencion = new Atencion(nombre, precio);
            HttpResponseMessage res = await http.PostAsJsonAsync("api/atenciones", atencion);
            res.EnsureSuccessStatusCode();
        }
    }
}


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMD_TechX.Controladores;
using OMD_TechX.Controladores.Validaciones;
using OMD_TechX.Helpers;
using OMD_TechX.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;

namespace OMD_TechX.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public HttpClient http;

        public RegisterModel(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:7083/");
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            //aca comienza la creacion

            //nombre
            [Required(ErrorMessage = "El campo Nombre es requerido.")]
            [StringLength(50, ErrorMessage = "El campo Nombre admite hasta 50 caracteres")]
            [Display(Name = "Nombre")]
            [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Ingresa solo letras")]
            public string Nombre { get; set; }

            //apellido
            [Required(ErrorMessage = "El campo Apellido es requerido.")]
            [StringLength(50, ErrorMessage = "El campo Apellido admite hasta 50 caracteres")]
            [Display(Name = "Apellido")]
            [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Ingresa solo letras")]
            public string Apellido { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El campo Email es requerido.")]
            [EmailAddress(ErrorMessage = "Este formato no es válido")]
            [EmailUnico]
            [Display(Name = "Email")]
            [StringLength(50, ErrorMessage = "El campo Email admite hasta 50 caracteres")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El campo DNI es requerido.")]
            [Display(Name = "DNI")]
            [DniUnico]
            [StringLength(maximumLength:8, MinimumLength =8, ErrorMessage = "El campo DNI debe tener 8 caracteres")]
            
            public string DNI { get; set; }
            [Required(ErrorMessage = "El campo Telefono es requerido.")]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Telefono")]
            [StringLength(30, ErrorMessage = "El campo Teléfono admite hasta 30 caracteres")]
            public string Telefono { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 

            /*[DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }*/
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
                /*http.DefaultRequestHeaders.Add("ADMIN", "true");
                var res = await http.GetAsync("https://localhost:7083/api/usuarios/" + Input.DNI);
                if (res.IsSuccessStatusCode)
                {*/
                    var user = CreateUser();
                    await _userManager.AddToRoleAsync(user, "Normal");

                    string password = crearPasswordAleatoria();
                    string password2 = password;

                    await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = Normal },
                            protocol: Request.Scheme);
                        registrarUsuario(userId, Input.Nombre, Input.Apellido, Input.DNI, Input.Email, Input.Telefono, password2);
                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        /*if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }*/
                        return LocalRedirect("/usuarios");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                /*}
                else
                {
                    ModelState.AddModelError(string.Empty, $"El DNI {Input.DNI} ya se encuentra registrado.");
                }*/
                

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
        async Task registrarUsuario(string id, string nombre, string apellido, string DNI, string email, string tel, string password)
        {
            Usuario usuario = new Usuario(id, nombre, apellido, DNI, email, tel);
            HttpResponseMessage res = await http.PostAsJsonAsync("api/usuarios", usuario);
            string name = nombre + " " + apellido;
            CorreoElectronico.enviarCorreo(email, name, password);
            res.EnsureSuccessStatusCode();
        }

        private string crearPasswordAleatoria()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 15;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }
    }
}

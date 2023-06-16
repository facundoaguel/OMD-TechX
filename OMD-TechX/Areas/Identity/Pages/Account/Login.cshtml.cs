// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OMD_TechX.Modelos;
using OMD_TechX.Controladores.Validaciones;
using OMD_TechX.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.JSInterop;
using Microsoft.CodeAnalysis.Scripting;
using System.Security.Principal;

namespace OMD_TechX.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient http;
        private readonly ApplicationDbContext context;
        private readonly IJSRuntime _jsRuntime;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, ApplicationDbContext context, IJSRuntime jsRuntime)
        {
            this.context = context;
            _signInManager = signInManager;
            _logger = logger;
            http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:7083/");
            _jsRuntime = jsRuntime;
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage ="El campo email es requerido")]
            [EmailAddress (ErrorMessage ="La dirección ingresada no es válida")]
            [EmailExistente]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage ="El campo contraseña es requerido")]
            [DataType(DataType.Password)]
            //[Remote(action: "VerificarContra", controller:"Usuarios", HttpMethod = "POST", AdditionalFields = nameof(Email))]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Recordarme")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var usuarios = await http.GetFromJsonAsync<List<Usuario>>($"api/usuarios");

                Usuario usuario = usuarios.Where(u => u.eliminado == false).FirstOrDefault( u => u.Email == Input.Email);

                if (result.Succeeded)
                {
                    _logger.LogInformation("El usuario se encuentra logueado");
                    if (usuario != null && usuario.primerInicio)
                    {
                        return RedirectToPage("./Manage/ChangePassword");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    //si llego hasta aca y el mail no es inexistente entonces la contrasenia es incorrecta
                    if(usuario != null  || Input.Email.Equals("pedro@omd.com"))
                    {
                        ModelState.AddModelError(Input.Password, "Contraseña incorrecta.");
                    }
                    return Page();
                }
                /*if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("La cuenta del usuario fue bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento fallido de iniciar sesion.");
                    return Page();
                }*/
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

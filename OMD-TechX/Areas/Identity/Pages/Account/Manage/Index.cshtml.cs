﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OMD_TechX.Modelos;
using static System.Net.WebRequestMethods;
using System.Net.Http;

namespace OMD_TechX.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HttpClient http;

        

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            http = new HttpClient();
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }
        public string Apellido { get; set; }

        public string DNI { get; set; }
        public string Telefono { get; set; }
        public List<Perro> Perros { get; set; } = new List<Perro>();

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /*[Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }*/

        }

        private async Task LoadAsync(IdentityUser user)
        {
            

            //var userName = usuario.Nombre;
            //var phoneNumber = usuario.Telefono;

            //Username = userName;
            if (!user.Email.Equals("pedro@omd.com"))
            {
                var usuario = await http.GetFromJsonAsync<Usuario>($"https://localhost:7083/api/usuarios/{user.Id}");
                Nombre = usuario.Nombre;
                Apellido = usuario.Apellido;
                Email = usuario.Email;
                DNI = usuario.DNI;
                Telefono = usuario.Telefono;
                Perros = usuario.Perros;
            }
            else
            {
                Nombre = "Pedro";
                Apellido = "Oh My Dog";
                Email = user.Email;
                DNI = "";
                Telefono = "";
                Perros = new List<Perro>();
            }

           


            /*Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };*/
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            /*var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }*/

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Tu perfil ha sido actualizado.";
            return RedirectToPage();
        }

        
    }
}

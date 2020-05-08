using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RedeSocial.Apresentacao.Controllers;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UsuarioController _usuarioController;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, 
            UsuarioController usuarioController)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioController = usuarioController;
        }

        [Display(Name = "Email")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Nome { get; set; }
            public string Sobrenome { get; set; }

            [Display(Name = "CPF")]
            public long Cpf { get; set; }

            [Display(Name = "Data de Nascimento")]
            [DataType(DataType.Date)]
            public DateTime DataNascimento { get; set; }
        }


        private async Task LoadAsync(IdentityUser user, UsuarioModel usuarioModel)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var cpf = usuarioModel.Cpf;
            var nome = usuarioModel.Nome;
            var sobrenome = usuarioModel.Sobrenome;
            var dataNascimento = usuarioModel.DataNascimento;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Cpf = cpf,
                Nome = nome,
                Sobrenome = sobrenome,
                DataNascimento = dataNascimento
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userModel = await _usuarioController.Details(user.Id);

            await LoadAsync(user, userModel);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userModel = await _usuarioController.Details(user.Id);

            if (!ModelState.IsValid)
            {
                await LoadAsync(user, userModel);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            userModel.Cpf = Input.Cpf;
            userModel.Nome = Input.Nome;
            userModel.Sobrenome = Input.Sobrenome;
            userModel.DataNascimento = Input.DataNascimento;
            userModel.FotoPerfil = null;



            await _usuarioController.Edit(user.Id, userModel);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }

            }

            

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

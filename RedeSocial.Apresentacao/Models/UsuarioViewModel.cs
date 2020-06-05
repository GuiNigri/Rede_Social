using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class UsuarioViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Digite a senha novamente")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public long Cpf { get; set; }

        public string FotoPerfil { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string IdentityUser { get; set; }
    }
}

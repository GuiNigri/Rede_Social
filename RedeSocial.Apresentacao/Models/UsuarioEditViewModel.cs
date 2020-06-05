using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class UsuarioEditViewModel
    {
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

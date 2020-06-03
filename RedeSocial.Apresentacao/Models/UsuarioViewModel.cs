using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public long Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string IdentityUser { get; set; }
    }
}

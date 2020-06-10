using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Perfil { get; set; }
        public string Foto { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models
{
    public class PerfilViewModel
    {
        public string IdentityUserLogado { get; set; }
        public string IdentityUserPerfil { get; set; }
        public string NomePerfil { get; set; }
        public string FotoPerfil { get; set; }
        public IEnumerable<AmigosViewModel> Amigos { get; set; }
        public int StatusAmizade { get; set; }

        public IEnumerable<PostViewModel> ListaPost;
    }
}

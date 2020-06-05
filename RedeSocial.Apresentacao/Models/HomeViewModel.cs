using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class HomeViewModel
    {
        public string IdentityUser { get; set; }
        public string NomePerfil { get; set; }
        public string FotoPerfil { get; set; }
        public int Seguidores { get; set; }
        public int Seguindo { get; set; }

        public IEnumerable<PostViewModel> ListaPost;
    }
}

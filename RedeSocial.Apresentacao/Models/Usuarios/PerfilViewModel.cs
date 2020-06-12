using System.Collections.Generic;
using RedeSocial.Apresentacao.Models.Amigos;
using RedeSocial.Apresentacao.Models.Posts;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Usuarios
{
    public class PerfilViewModel
    {
        public int IdAmizade { get; set; }
        public string IdentityUserLogado { get; set; }
        public string IdentityUserPerfil { get; set; }
        public string NomePerfil { get; set; }
        public string FotoPerfil { get; set; }
        public IEnumerable<AmigosViewModel> Amigos { get; set; }
        public int AmigosPerfilCount { get; set; }
        public int StatusAmizade { get; set; }

        public IEnumerable<PostViewModel> PostList;

        public PerfilViewModel(string userIdLogado, UsuarioModel usuarioPerfil,List<PostViewModel> postList,
            int statusAmizade, IEnumerable<AmigosViewModel> amigosList, int idAmizade, int amigosCount)
        {
            IdentityUserLogado = userIdLogado;
            IdentityUserPerfil = usuarioPerfil.IdentityUser;
            NomePerfil = usuarioPerfil.Nome + " " + usuarioPerfil.Sobrenome;
            FotoPerfil = usuarioPerfil.FotoPerfil;
            PostList = postList;
            StatusAmizade = statusAmizade;
            Amigos = amigosList;
            IdAmizade = idAmizade;
            AmigosPerfilCount = amigosCount;

        }
    }
}

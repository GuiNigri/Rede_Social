using System.Collections.Generic;
using RedeSocial.Apresentacao.Models.Amigos;
using RedeSocial.Apresentacao.Models.Posts;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Home
{
    public class HomeViewModel
    {
        public string IdentityUserLogado { get; set; }
        public string NomePerfil { get; set; }
        public string FotoPerfil { get; set; }
        public IEnumerable<AmigosViewModel> Amigos { get; set; }

        public IEnumerable<AmigosViewModel> SolicitacoesPendentes { get; set; }

        public IEnumerable<PostViewModel> PostList;

        public HomeViewModel(string userId, UsuarioModel usuarioModel, List<PostViewModel> postList,
        IEnumerable<AmigosViewModel> solicitacoes, IEnumerable<AmigosViewModel> amigosList)
        {
            IdentityUserLogado = userId;
            NomePerfil = usuarioModel.Nome + " " + usuarioModel.Sobrenome;
            FotoPerfil = usuarioModel.FotoPerfil;
            PostList = postList;
            SolicitacoesPendentes = solicitacoes;
            Amigos = amigosList;
        }
    }
}

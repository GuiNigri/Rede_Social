using System.Collections.Generic;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Texto { get; set; }
        public string UriImage { get; set; }
        public int Privacidade { get; set; }
        public int TempoDaPostagem { get; set; }
        public string FormatacaoTempo { get; set; }
        public string IdentityUser { get; set; }
        public string FotoPerfil { get; set; }
        public int QuantLikes { get; set; }
        public bool StatusLike { get; set; }
        public IEnumerable<CommentPostViewModel> CommentList { get; set; }

        public PostViewModel(PostModel postModel, UsuarioModel usuarioModel, int tempo,
            string formato, IEnumerable<CommentPostViewModel> listaComment, int quantLikes, bool statusLike)
        {
            Id = postModel.Id;
            NomeCompleto = usuarioModel.Nome + " " + usuarioModel.Sobrenome;
            Privacidade = postModel.Privacidade;
            Texto = postModel.Texto;
            UriImage = postModel.UriImage;
            TempoDaPostagem = tempo;
            FormatacaoTempo = formato;
            IdentityUser = postModel.IdentityUser;
            FotoPerfil = usuarioModel.FotoPerfil;
            CommentList = listaComment;
            QuantLikes = quantLikes;
            StatusLike = statusLike;
        }
    }

}

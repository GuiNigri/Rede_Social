using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Posts
{
    public class CommentPostViewModel
    {
        public int Id { get; set; }
        public string IdentityUser { get; set; }
        public string Comment { get; set; }
        public string Nome { get; set; }
        public int TempoDoComentario{ get; set; }
        public string FormatacaoTempo { get; set; }
        public string FotoPerfil { get; set; }

        public CommentPostViewModel(CommentPostModel commentPostModel, UsuarioModel usuarioModel,
            int tempo, string formato)
        {
            Comment = commentPostModel.Comment;
            IdentityUser = commentPostModel.IdentityUser;
            Nome = usuarioModel.Nome + " " + usuarioModel.Sobrenome;
            TempoDoComentario = tempo;
            FormatacaoTempo = formato;
            FotoPerfil = usuarioModel.FotoPerfil;
            Id = commentPostModel.Id;

        }
    }
}

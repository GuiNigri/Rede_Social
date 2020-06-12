using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Amigos
{
    public class AmigosViewModel
    {
        public int Id { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Perfil { get; private set; }
        public string Foto { get; private set; }

        private AmigosViewModel(AmigosModel amigosModel)
        {
            Id = amigosModel.Id;
        }

        public AmigosViewModel(AmigosModel amigosModel, UsuarioModel usuarioModel):this(amigosModel)
        {
            Foto = usuarioModel.FotoPerfil;
            NomeCompleto = usuarioModel.Nome + " " + usuarioModel.Sobrenome;
            Perfil = usuarioModel.IdentityUser;
        }

    }
}

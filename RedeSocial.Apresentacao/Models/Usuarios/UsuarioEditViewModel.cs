using System;
using System.ComponentModel.DataAnnotations;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models.Usuarios
{
    public class UsuarioEditViewModel
    {
        public int Id { get; set; }
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

        public UsuarioEditViewModel(UsuarioModel usuarioModel)
        {
            IdentityUser = usuarioModel.IdentityUser;
            Cpf = usuarioModel.Cpf;
            DataNascimento = usuarioModel.DataNascimento;
            Nome = usuarioModel.Nome;
            Sobrenome = usuarioModel.Sobrenome;
            FotoPerfil = usuarioModel.FotoPerfil;
            Id = usuarioModel.Id;
        }
    }
}

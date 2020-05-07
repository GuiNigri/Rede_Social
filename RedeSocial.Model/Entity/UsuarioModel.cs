
using System;
using Microsoft.AspNetCore.Identity;


namespace RedeSocial.Model.Entity
{
    public class UsuarioModel
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public long Cpf { get; set; }

        public DateTime DataNascimento { get; set; }
        public Uri FotoPerfil { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}

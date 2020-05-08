
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace RedeSocial.Model.Entity
{
    public class UsuarioModel
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public long Cpf { get; set; }

        public DateTime DataNascimento { get; set; }
        public Uri FotoPerfil { get; set; }
        [Key]
        public string IdentityUser { get; set; }
    }
}

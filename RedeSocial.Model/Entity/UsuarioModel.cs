
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Identity;


namespace RedeSocial.Model.Entity
{
    public class UsuarioModel:BaseModel
    {

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public long Cpf { get; set; }

        public DateTime DataNascimento { get; set; }
        public string FotoPerfil { get; set; }

        
        public string IdentityUser { get; set; }
    }

    public class CreateAndUpdateHttpUsuarioModel
    {
        public UsuarioModel UsuarioModel { get; set; }
        public string ImageBase64 { get; set; }
    }
}

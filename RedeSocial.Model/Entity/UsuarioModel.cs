using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class UsuarioModel:IdentityUser
    {

        [PersonalData]
        public string Nome { get; set; }

        [PersonalData]
        public string Sobrenome { get; set; }

        [PersonalData]
        public long Cpf { get; set; }

        [PersonalData]
        public DateTime DataNascimento { get; set; }
    }
}

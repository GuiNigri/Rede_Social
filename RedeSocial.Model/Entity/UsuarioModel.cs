using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models
{
    public class UsuarioDetailsViewModel
    {
        public IEnumerable<UsuarioModel> listaUsuarios { get; set; }
    }
}

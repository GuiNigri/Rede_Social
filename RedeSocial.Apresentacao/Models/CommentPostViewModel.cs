using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Texto { get; set; }
        public string UriImage { get; set; }
        public int Privacidade { get; set; }
        public int TempoDaPostagem { get; set; }
        public string FormatacaoTempo { get; set; }
    }
}

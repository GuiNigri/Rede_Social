using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Options
{
    public class ProjetoHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string UsuarioPath { get; set; }
        public string PostPath { get; set; }
        public string AmigosPath { get; set; }
        public string CommentPostPath { get; set; }
        public string LikePostPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}

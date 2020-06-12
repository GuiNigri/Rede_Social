using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Models
{
    public class CommentPostInputModel
    {
        public string IdentityUser { get; set; }
        public int PostModelId { get; set; }
        public string Comment { get; set; }
        public string DataComentario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class PostModel:BaseModel
    {
        public string IdentityUser { get; set; }
        public string Texto { get; set; }
        public string UriImage { get; set; }
        public int Privacidade { get; set; }
        public DateTime DataPostagem { get; set; }
    }

    public class CommentPostModel : BaseModel
    {
        public string IdentityUser { get; set; }
        public string Comment { get; set; }
        public DateTime DataDoComment { get; set; }
        public int PostModelId { get; set; }
        public PostModel Post { get; set; }
        
    }

    public class LikePostModel : BaseModel
    {
        public string IdentityUser { get; set; }
        public int PostModelId { get; set; }
        public PostModel Post { get; set; }
        
    }

}

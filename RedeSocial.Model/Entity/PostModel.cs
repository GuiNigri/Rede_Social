using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUser { get; set; }
        public string Texto { get; set; }
        public string UriImage { get; set; }
        public int Privacidade { get; set; }
        public DateTime DataPostagem { get; set; }
    }

    public class CommentPostModel
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUser { get; set; }
        public PostModel IdPostModel { get; set; }
        public string Comment { get; set; }
    }

    public class LikesPostModel
    {
        [Key]
        public int Id { get; set; }
        public string IdentityUser { get; set; }
        public PostModel IdPostModel { get; set; }
        public int MyProperty { get; set; }
    }

}

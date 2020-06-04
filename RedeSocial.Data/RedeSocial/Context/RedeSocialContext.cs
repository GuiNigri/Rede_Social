using Microsoft.EntityFrameworkCore;

namespace RedeSocial.Data.RedeSocial.Context
{
    public class RedeSocialContext : DbContext
    {
        public RedeSocialContext (DbContextOptions<RedeSocialContext> options)
            : base(options)
        {
        }

        public DbSet<Model.Entity.UsuarioModel> UsuarioModel { get; set; }
        public DbSet<Model.Entity.PostModel> PostModel { get; set; }
        public DbSet<Model.Entity.CommentPostModel> CommentPostModel { get; set; }
        public DbSet<Model.Entity.LikesPostModel> LikesPostModel { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using RedeSocial.Model.Entity;

namespace RedeSocial.Data.RedeSocial.Context
{
    public class RedeSocialContext : DbContext
    {
        public RedeSocialContext (DbContextOptions<RedeSocialContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> UsuarioModel { get; set; }
        public DbSet<PostModel> PostModel { get; set; }
        public DbSet<AmigosModel> AmigosModel { get; set; }
        public DbSet<CommentPostModel> CommentPostModel { get; set; }
        public DbSet<LikesPostModel> LikesPostModel { get; set; }
    }
}

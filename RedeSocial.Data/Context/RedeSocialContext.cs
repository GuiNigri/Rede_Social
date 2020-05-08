using Microsoft.EntityFrameworkCore;

namespace RedeSocial.Data.Context
{
    public class RedeSocialContext : DbContext
    {
        public RedeSocialContext (DbContextOptions<RedeSocialContext> options)
            : base(options)
        {
        }

        public DbSet<RedeSocial.Model.Entity.UsuarioModel> UsuarioModel { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityDbContext = RedeSocial.Data.Entity.Data.IdentityDbContext;


namespace RedeSocial.IoC
{
    public static class IdentityRegistration
    {
        public static void RegisterIdentity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("IdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                     .AddEntityFrameworkStores<IdentityDbContext>();
        }
    }
}
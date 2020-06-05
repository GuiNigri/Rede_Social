using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace RedeSocial.IoC
{
    public static class IdentityRegistration
    {
        public static void RegisterIdentityForMvc(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                     .AddEntityFrameworkStores<IdentityDbContext>();
        }

        public static void RegisterIdentityForWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);
        
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityDbContext>();
        }
        
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDbContextConnection")));
        }
    }
}
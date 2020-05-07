
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Identity;
using RedeSocial.Model.Entity;
using IdentityDbContext = RedeSocial.Identity.Data.IdentityDbContext;


[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace RedeSocial.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => 
            {

                services.AddDbContext<IdentityDbContext>(options => 
                    options.UseSqlServer(context.Configuration.GetConnectionString("IdentityDbContextConnection")));

                //services.AddIdentity<IdentityUser, IdentityRole>()
                //    .AddEntityFrameworkStores<IdentityDbContext>();

               services.AddIdentity<IdentityUser, IdentityRole>()
                   .AddEntityFrameworkStores<IdentityDbContext>()
                   .AddDefaultTokenProviders()
                   .AddDefaultUI();
               
               services.AddIdentityCore<UsuarioModel>()
                   .AddRoles<IdentityRole>()
                   .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<UsuarioModel, IdentityRole>>()
                   .AddEntityFrameworkStores<IdentityDbContext>()
                   .AddDefaultTokenProviders()
                   .AddDefaultUI();


            });
        }
    }
}
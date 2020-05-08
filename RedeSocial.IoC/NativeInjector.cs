using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Data;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Services;

namespace RedeSocial.IoC
{
    public static class NativeInjector
    {
        public static void RegisterInjections(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<RedeSocialContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RedeSocialContext")));

            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Data;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Infraestrutura.Blob;
using RedeSocial.Model.Interfaces.Blob;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Services;

namespace RedeSocial.IoC
{
    public static class NativeInjector
    {
        public static void RegisterInjectionsForWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IPostServices, PostServices>();

            AddDbContextAndData(services,configuration);
            
        }

        private static void AddDbContextAndData(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RedeSocialContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RedeSocialContext")));
            
            services.AddTransient<IBlobServices, BlobServices>(provider => 
                new BlobServices(configuration.GetConnectionString("StorageAccount")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPostRepository, PostRepository>();


        }
    }
}

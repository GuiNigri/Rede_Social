using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Apresentacao.HttpServices;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.Options;

namespace RedeSocial.Apresentacao.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var projetoHttpOptionsSection = configuration.GetSection(nameof(ProjetoHttpOptions));
            var projetoHttpOptions = projetoHttpOptionsSection.Get<ProjetoHttpOptions>();

            services.AddHttpClient(projetoHttpOptions.Name, x => { x.BaseAddress = projetoHttpOptions.ApiBaseUrl; });

            services.AddTransient<IUsuarioServices, UsuarioHttpServices>();
            services.AddTransient<IPostServices, PostHttpServices>();
            services.AddTransient<IAmigosServices, AmigosHttpServices>();
            services.AddTransient<ICommentPostServices, CommentPostHttpServices>();
            services.AddTransient<ILikePostServices, LikePostHttpServices>();
        }

    }
}

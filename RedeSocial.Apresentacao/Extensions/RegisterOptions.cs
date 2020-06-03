using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Model.Options;

namespace RedeSocial.Apresentacao.Extensions
{
    public static class RegisterOptions
    {
        public static void RegisterConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProjetoHttpOptions>(configuration.GetSection(nameof(ProjetoHttpOptions)));
        }
    }
}

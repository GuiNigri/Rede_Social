using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IUsuarioServices:IBaseServices<UsuarioModel>, IUsuarioRepository
    {
        Task CreateAsync(UsuarioModel usuarioModel, string base64);
        Task UpdateAsync(UsuarioModel usuarioModel, string base64);
    }
}

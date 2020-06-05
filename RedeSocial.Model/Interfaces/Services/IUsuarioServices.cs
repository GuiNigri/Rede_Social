using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IUsuarioServices
    {
        Task CreateAsync(UsuarioModel usuarioModel, string base64);
        Task UpdateAsync(UsuarioModel usuarioModel, string base64);
        Task DeleteAsync(string id);
        Task<IEnumerable<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> GetByIdAsync(string id);
        //Task<bool> GetByCpfAsync(long cpf);
    }
}

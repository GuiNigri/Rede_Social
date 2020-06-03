using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IUsuarioServices
    {
        Task CreateAsync(UsuarioModel usuarioModel, string stream);
        Task UpdateAsync(UsuarioModel usuarioModel);
        Task DeleteAsync(string id);
        Task<IEnumerable<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> GetByIdAsync(string id);
        //Task<bool> GetByCpfAsync(long cpf);
    }
}

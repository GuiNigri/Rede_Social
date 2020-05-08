using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task CreateAsync(UsuarioModel usuarioModel);
        Task UpdateAsync(UsuarioModel usuarioModel);
        Task DeleteAsync(UsuarioModel usuarioModel);
        Task<IEnumerable<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> GetByIdAsync(string Id);
        bool UsuarioModelExists(string id);
        Task<bool> GetByCpfAsync(long CPF);
    }
}

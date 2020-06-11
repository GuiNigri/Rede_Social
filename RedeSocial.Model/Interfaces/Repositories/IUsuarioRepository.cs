using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IUsuarioRepository:IBaseRepository<UsuarioModel>
    {
        Task<UsuarioModel> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}

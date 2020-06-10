using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IAmigosRepository
    {
        Task CreateAsync(AmigosModel amigosModel);
        Task UpdateAsync(AmigosModel amigosModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<AmigosModel>> GetAllAsync();
        Task<AmigosModel> GetByUsersAsync(string userLogado, string perfilAcessado);
        Task<AmigosModel> GetByIdAsync(int id);
        Task<IEnumerable<AmigosModel>> GetSolicitacoesPendentes(string userLogado);
        Task<IEnumerable<AmigosModel>> GetListByUserAsync(string userLogado);
    }
}

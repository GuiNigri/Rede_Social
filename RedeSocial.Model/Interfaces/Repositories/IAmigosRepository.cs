using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IAmigosRepository:IBaseRepository<AmigosModel>
    {
        Task<AmigosModel> GetByUsersAsync(string userLogado, string perfilAcessado);
        Task<IEnumerable<AmigosModel>> GetSolicitacoesPendentes(string userLogado);
        Task<IEnumerable<AmigosModel>> GetListByUserAsync(string userLogado);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class AmigosServices: BaseServices<AmigosModel>, IAmigosServices
    {
        private readonly IAmigosRepository _amigosRepository;

        public AmigosServices(IAmigosRepository amigosRepository):base(amigosRepository)
        {
            _amigosRepository = amigosRepository;
        }

        public async Task<AmigosModel> GetByUsersAsync(string userLogado, string perfilAcessado)
        {
            return await _amigosRepository.GetByUsersAsync(userLogado, perfilAcessado);
        }

        public async Task<IEnumerable<AmigosModel>> GetSolicitacoesPendentes(string userLogado)
        {
            return await _amigosRepository.GetSolicitacoesPendentes(userLogado);
        }

        public async Task<IEnumerable<AmigosModel>> GetListByUserAsync(string userLogado)
        {
            return await _amigosRepository.GetListByUserAsync(userLogado);
        }
    }
}

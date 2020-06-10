using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class AmigosServices:IAmigosServices
    {
        private readonly IAmigosRepository _amigosRepository;

        public AmigosServices(IAmigosRepository amigosRepository)
        {
            _amigosRepository = amigosRepository;
        }
        public async Task CreateAsync(AmigosModel amigosModel)
        {
            await _amigosRepository.CreateAsync(amigosModel);
        }

        public async Task UpdateAsync(AmigosModel amigosModel)
        {

            await _amigosRepository.UpdateAsync(amigosModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _amigosRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AmigosModel>> GetAllAsync()
        {
            return await _amigosRepository.GetAllAsync();
        }

        public async Task<AmigosModel> GetByUserAsync(string userLogado, string perfilAcessado)
        {
            return await _amigosRepository.GetByUsersAsync(userLogado, perfilAcessado);
        }

        public async Task<AmigosModel> GetByIdAsync(int id)
        {
            return await _amigosRepository.GetByIdAsync(id);
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

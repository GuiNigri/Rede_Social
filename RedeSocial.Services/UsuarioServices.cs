using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class UsuarioServices:IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task CreateAsync(UsuarioModel usuarioModel)
        {
           await _usuarioRepository.CreateAsync(usuarioModel);
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel)
        {
            await _usuarioRepository.UpdateAsync(usuarioModel);
        }

        public async Task DeleteAsync(UsuarioModel usuarioModel)
        {
            await _usuarioRepository.DeleteAsync(usuarioModel);
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<UsuarioModel> GetByIdAsync(string Id)
        {
            return await _usuarioRepository.GetByIdAsync(Id);
        }

        public bool UsuarioModelExists(string id)
        {
            return _usuarioRepository.UsuarioModelExists(id);
        }

        public async Task<bool> GetByCpfAsync(long CPF)
        {
            return await _usuarioRepository.GetByCpfAsync(CPF);
        }
    }
}

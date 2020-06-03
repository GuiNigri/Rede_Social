using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Blob;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class UsuarioServices:IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IBlobServices _blobServices;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IBlobServices blobServices)
        {
            _usuarioRepository = usuarioRepository;
            _blobServices = blobServices;
        }
        public async Task CreateAsync(UsuarioModel usuarioModel, string imageBase64)
        {
            

            var blob = await _blobServices.CreateBlobAsync(imageBase64);

            usuarioModel.FotoPerfil = blob;

            await _usuarioRepository.CreateAsync(usuarioModel);
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel)
        {
            await _usuarioRepository.UpdateAsync(usuarioModel);
        }

        public async Task DeleteAsync(string id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<UsuarioModel> GetByIdAsync(string id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }


    }
}

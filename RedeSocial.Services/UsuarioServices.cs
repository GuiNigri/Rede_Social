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
        private readonly IPostServices _postServices;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IBlobServices blobServices, IPostServices postServices)
        {
            _usuarioRepository = usuarioRepository;
            _blobServices = blobServices;
            _postServices = postServices;
        }
        public async Task CreateAsync(UsuarioModel usuarioModel, string base64)
        {
            if(base64 != null)
            {
                var blob = await _blobServices.CreateBlobAsync(base64);

                usuarioModel.FotoPerfil = blob;
            }


            await _usuarioRepository.CreateAsync(usuarioModel);
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel, string base64)
        {
            if(base64 != null)
            {
                if(usuarioModel.FotoPerfil != null)
                {
                    await _blobServices.DeleteBlobAsync(usuarioModel.FotoPerfil);
                }
                
                var blob = await _blobServices.CreateBlobAsync(base64);

                usuarioModel.FotoPerfil = blob;
            }
            
            await _usuarioRepository.UpdateAsync(usuarioModel);
        }

        public async Task DeleteAsync(string id)
        {
            var postagens = await _postServices.GetPostsByUserAsync(id);

            foreach (var post in postagens)
            {
                await _postServices.DeleteAsync(post.Id, post.UriImage);
            }

            var user = await _usuarioRepository.GetByIdAsync(id);

            await _blobServices.DeleteBlobAsync(user.FotoPerfil);

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

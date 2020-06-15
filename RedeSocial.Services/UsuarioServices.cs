using System.Collections.Generic;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Blob;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class UsuarioServices:BaseServices<UsuarioModel>, IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IBlobServices _blobServices;
        private readonly IPostServices _postServices;
        private readonly ICommentPostServices _commentPostRepository;
        private readonly ILikePostRepository _likePostRepository;
        private readonly IAmigosRepository _amigosRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IBlobServices blobServices, IPostServices postServices, ICommentPostServices commentPostRepository, ILikePostRepository likePostRepository,IAmigosRepository amigosRepository):base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _blobServices = blobServices;
            _postServices = postServices;
            _commentPostRepository = commentPostRepository;
            _likePostRepository = likePostRepository;
            _amigosRepository = amigosRepository;
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

            var comentarios = await _commentPostRepository.GetCommentByUserAsync(id);

            var likes = await _likePostRepository.GetLikeByUserAsync(id);

            var amigos = await _amigosRepository.GetAllByUserAsync(id);

            var user = await _usuarioRepository.GetByIdAsync(id);

            foreach (var post in postagens)
            {
                await _postServices.DeleteAsync(post.Id, post.UriImage);
            }

            foreach (var comentario in comentarios)
            {
                await _commentPostRepository.DeleteAsync(comentario.Id);
            }

            foreach (var like in likes)
            {
                await _likePostRepository.DeleteAsync(like.Id);
            }

            foreach (var amigo in amigos)
            {
                await _amigosRepository.DeleteAsync(amigo.Id);
            }

            await _blobServices.DeleteBlobAsync(user.FotoPerfil);

            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioModel>> GetFiltroAsync(string termoInputado)
        {
            return await _usuarioRepository.GetFiltroAsync(termoInputado);
        }

        public async Task<UsuarioModel> GetByIdAsync(string id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }


    }
}

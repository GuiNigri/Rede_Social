using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Blob;
using RedeSocial.Model.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class PostServices : BaseServices<PostModel>, IPostRepository, IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlobServices _blobServices;
        private readonly ICommentPostServices _commentPostServices;
        private readonly ILikePostServices _likePostServices;

        public PostServices(IPostRepository postRepository, IBlobServices blobServices, ICommentPostServices commentPostServices, ILikePostServices likePostServices):base(postRepository)
        {
            _postRepository = postRepository;
            _blobServices = blobServices;
            _commentPostServices = commentPostServices;
            _likePostServices = likePostServices;
        }
        public override async Task CreateAsync(PostModel postModel)
        {
            if(postModel.UriImage != null)
            {
                var blob = await _blobServices.CreateBlobAsync(postModel.UriImage);

                postModel.UriImage = blob;
            }

            await _postRepository.CreateAsync(postModel);
        }

        public async Task DeleteAsync(int id, string uri)
        {
            var comentarios = await _commentPostServices.GetPostByIdAsync(id);

            var likes = await _likePostServices.GetPostByIdAsync(id);


            foreach (var comentario in comentarios)
            {
                await _commentPostServices.DeleteAsync(comentario.Id);
            }

            foreach (var like in likes)
            {
                await _likePostServices.DeleteAsync(like.Id);
            }

            await _postRepository.DeleteAsync(id);

            if (uri != null)
            {
                await _blobServices.DeleteBlobAsync(uri);
            }
        }


        public async Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id)
        {
            return await _postRepository.GetPostsByUserAsync(id);
        }
    }
}

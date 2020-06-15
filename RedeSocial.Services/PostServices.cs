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
        private readonly ICommentPostRepository _commentPostRepository;
        private readonly ILikePostRepository _likePostRepository;


        public PostServices(IPostRepository postRepository, IBlobServices blobServices, ICommentPostRepository commentPostRepository, ILikePostRepository likePostRepository):base(postRepository)
        {
            _postRepository = postRepository;
            _blobServices = blobServices;
            _commentPostRepository = commentPostRepository;
            _likePostRepository = likePostRepository;

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
            var comentarios = await _commentPostRepository.GetPostByIdAsync(id);

            var likes = await _likePostRepository.GetPostByIdAsync(id);


            foreach (var comentario in comentarios)
            {
                await _commentPostRepository.DeleteAsync(comentario.Id);
            }

            foreach (var like in likes)
            {
                await _likePostRepository.DeleteAsync(like.Id);
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

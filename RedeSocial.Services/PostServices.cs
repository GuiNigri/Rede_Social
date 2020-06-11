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

        public PostServices(IPostRepository postRepository, IBlobServices blobServices):base(postRepository)
        {
            _postRepository = postRepository;
            _blobServices = blobServices;
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
            if(uri != null)
            {
                await _blobServices.DeleteBlobAsync(uri);
            }

            await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id)
        {
            return await _postRepository.GetPostsByUserAsync(id);
        }
    }
}

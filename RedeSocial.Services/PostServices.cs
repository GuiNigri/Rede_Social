using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Blob;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Services
{
    public class PostServices : IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlobServices _blobServices;

        public PostServices(IPostRepository postRepository, IBlobServices blobServices)
        {
            _postRepository = postRepository;
            _blobServices = blobServices;
        }
        public async Task CreateAsync(PostModel postModel)
        {
            var blob = await _blobServices.CreateBlobAsync(postModel.UriImage);

            postModel.UriImage = blob;

           await _postRepository.CreateAsync(postModel);
        }

        public Task<IEnumerable<PostModel>> GetAll()
        {
            return _postRepository.GetAll();
        }
    }
}

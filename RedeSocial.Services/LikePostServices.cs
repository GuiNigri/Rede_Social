using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Services
{
    public class LikePostServices : BaseServices<LikePostModel>, ILikePostServices
    {
        private readonly ILikePostRepository _likePostRepository;

        public LikePostServices(ILikePostRepository likePostRepository):base(likePostRepository)
        {
            _likePostRepository = likePostRepository;
        }

        public async Task<IEnumerable<LikePostModel>> GetPostByIdAsync(int id)
        {
            return await _likePostRepository.GetPostByIdAsync(id);
        }

        public async Task<LikePostModel> GetStatusAsync(string userId, int idPost)
        {
            return await _likePostRepository.GetStatusAsync(userId, idPost);
        }

    }
}

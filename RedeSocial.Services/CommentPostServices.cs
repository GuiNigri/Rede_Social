using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedeSocial.Services
{
    public class CommentPostServices : BaseServices<CommentPostModel>,ICommentPostServices
    {
        private readonly ICommentPostRepository _commentPostRepository;

        public CommentPostServices(ICommentPostRepository commentPostRepository) : base(commentPostRepository)
        {
            _commentPostRepository = commentPostRepository;
        }

        public async Task<IEnumerable<CommentPostModel>> GetPostByIdAsync(int id)
        {
            return await _commentPostRepository.GetPostByIdAsync(id);
        }

        public async Task<IEnumerable<CommentPostModel>> GetCommentByUserAsync(string userId)
        {
            return await _commentPostRepository.GetCommentByUserAsync(userId);
        }
    }
}

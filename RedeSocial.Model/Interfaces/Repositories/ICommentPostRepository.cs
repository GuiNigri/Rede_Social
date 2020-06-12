using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface ICommentPostRepository:IBaseRepository<CommentPostModel>
    {
        Task<IEnumerable<CommentPostModel>> GetPostByIdAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface ILikePostRepository:IBaseRepository<LikePostModel>
    {
        Task<IEnumerable<LikePostModel>> GetPostByIdAsync(int id);
        Task<LikePostModel> GetStatusAsync(string userId, int idPost);
    }
}

using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IPostRepository:IBaseRepository<PostModel>
    {
        Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id);
    }
}

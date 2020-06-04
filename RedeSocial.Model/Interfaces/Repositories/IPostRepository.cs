using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostModel>> GetAll();
        Task CreateAsync(PostModel postModel);
    }
}

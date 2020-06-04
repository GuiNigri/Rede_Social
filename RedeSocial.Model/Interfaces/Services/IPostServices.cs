using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IPostServices
    {
        Task<IEnumerable<PostModel>> GetAll();
        Task CreateAsync(PostModel postModel);
    }
}

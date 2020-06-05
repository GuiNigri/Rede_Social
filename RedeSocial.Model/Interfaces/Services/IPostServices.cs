using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IPostServices
    {
        Task<IEnumerable<PostModel>> GetAllAsync();
        Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id);
        Task<PostModel> GetByidAsync(int id);
        Task CreateAsync(PostModel postModel);
        Task DeleteAsync(int id, string uri);
    }
}

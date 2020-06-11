using RedeSocial.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IPostServices : IBaseServices<PostModel>, IPostRepository
    {
        Task DeleteAsync(int id, string uri);
    }
}

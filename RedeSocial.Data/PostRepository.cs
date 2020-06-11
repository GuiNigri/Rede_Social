using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocial.Data
{
    public class PostRepository : BaseRepository<PostModel>, IPostRepository
    {
        private readonly RedeSocialContext _context;
        public PostRepository(RedeSocialContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id)
        {
            return await _context.PostModel.Where(x => x.IdentityUser == id).ToListAsync();
        }

        public override async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            return _context.PostModel.OrderByDescending(x => x.DataPostagem);
        }
    }
}

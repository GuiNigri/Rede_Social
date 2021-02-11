using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Data
{
    public class LikePostRepository : BaseRepository<LikePostModel>, ILikePostRepository
    {
        private readonly RedeSocialContext _context;

        public LikePostRepository(RedeSocialContext context):base(context)
        {
        }

        public async Task<IEnumerable<LikePostModel>> GetPostByIdAsync(int id)
        {
            return await _dbSet.Where(x => x.PostModelId == id).ToListAsync();
        }

        public async Task<LikePostModel> GetStatusAsync(string userId, int idPost)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.PostModelId == idPost && x.IdentityUser == userId);
        }

        public async Task<IEnumerable<LikePostModel>> GetLikeByUserAsync(string userId)
        {
            return await _dbSet.Where(x => x.IdentityUser == userId).ToListAsync();
        }
    }
}

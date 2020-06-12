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
            _context = context;
        }

        public async Task<IEnumerable<LikePostModel>> GetPostByIdAsync(int id)
        {
            return await _dbSet.Where(x => x.PostModelId == id).ToListAsync();
        }
        public override async Task<LikePostModel> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.PostModelId == id);
        }

        public async Task<LikePostModel> GetStatusAsync(string userId, int idPost)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.PostModelId == idPost && x.IdentityUser == userId);
        }

        public override async Task DeleteAsync(int id)
        {
            var likeModel = await _dbSet.FirstOrDefaultAsync(x => x.PostModelId == id);
            _dbSet.Remove(likeModel);
            await _context.SaveChangesAsync();
        }
    }
}

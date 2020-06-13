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
    public class CommentPostRepository:BaseRepository<CommentPostModel>, ICommentPostRepository
    {
        private readonly RedeSocialContext _context;

        public CommentPostRepository(RedeSocialContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentPostModel>> GetPostByIdAsync(int id)
        {
           return await _dbSet.Where(x => x.PostModelId == id).ToListAsync();
        }

        public async Task<IEnumerable<CommentPostModel>> GetCommentByUserAsync(string userId)
        {
            return await _dbSet.Where(x => x.IdentityUser == userId).ToListAsync();
        }
    }
}

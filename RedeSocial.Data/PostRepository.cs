using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly RedeSocialContext _context;
        public PostRepository(RedeSocialContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(PostModel postModel)
        {
            await _context.PostModel.AddAsync(postModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var postModel = await _context.PostModel.FindAsync(id);
            _context.PostModel.Remove(postModel);
            await _context.SaveChangesAsync();
        }

        public async Task<PostModel> GeByidAsync(int id)
        {
            return await _context.PostModel.FindAsync(id);
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            return _context.PostModel.OrderByDescending(x => x.DataPostagem);
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id)
        {
            return await _context.PostModel.Where(x => x.IdentityUser == id).ToListAsync();
        }
    }
}

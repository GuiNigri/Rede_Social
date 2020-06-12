using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Data
{
    public abstract class BaseRepository<TModel>:IBaseRepository<TModel> where TModel: BaseModel
    {
        protected readonly RedeSocialContext Context;
        protected readonly DbSet<TModel> _dbSet;

        protected BaseRepository(RedeSocialContext context)
        {
            Context = context;
            _dbSet = context.Set<TModel>();
        }
        public virtual async Task CreateAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            _dbSet.Update(model);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await _dbSet.FindAsync(id);
            _dbSet.Remove(model);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

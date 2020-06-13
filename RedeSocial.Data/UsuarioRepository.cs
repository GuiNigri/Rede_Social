using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Data
{
    public class UsuarioRepository: BaseRepository<UsuarioModel>, IUsuarioRepository
    {
        private readonly RedeSocialContext _context;

        public UsuarioRepository(RedeSocialContext context):base(context)
        {
            _context = context;
        }

        public override async Task CreateAsync(UsuarioModel usuarioModel)
        {
            await _context.UsuarioModel.AddAsync(usuarioModel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            var usuarioModel = await _context.UsuarioModel.FirstOrDefaultAsync(x => x.IdentityUser == id);
            _context.UsuarioModel.Remove(usuarioModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> GetFiltroAsync(string termoInputado)
        {
            return await _context.UsuarioModel.Where(x => x.Nome.Contains(termoInputado) || x.Sobrenome.Contains(termoInputado)).ToListAsync();
        }


        public async Task<UsuarioModel> GetByIdAsync(string id)
        {
            return await _context.UsuarioModel.FirstOrDefaultAsync(x => x.IdentityUser == id);
        }
    }
}

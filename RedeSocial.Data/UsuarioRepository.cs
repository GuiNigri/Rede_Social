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
    public class UsuarioRepository:IUsuarioRepository
    {
        private readonly RedeSocialContext _context;

        public UsuarioRepository(RedeSocialContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(UsuarioModel usuarioModel)
        {
            await _context.UsuarioModel.AddAsync(usuarioModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel)
        {
            _context.Entry(usuarioModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsuarioModel usuarioModel)
        {
            _context.UsuarioModel.Remove(usuarioModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            return await _context.UsuarioModel.ToListAsync();
        }

        public async Task<UsuarioModel> GetByIdAsync(string Id)
        {
            return await _context.UsuarioModel.FindAsync(Id); ;
        }

        public bool UsuarioModelExists(string id)
        {
            return _context.UsuarioModel.Any(e => e.IdentityUser == id);
        }

        public async Task<bool> GetByCpfAsync(long CPF)
        {
            return await _context.UsuarioModel.AnyAsync(x => x.Cpf == CPF);
        }
    }
}

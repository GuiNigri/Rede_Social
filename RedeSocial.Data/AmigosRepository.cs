using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Data
{
    public class AmigosRepository:IAmigosRepository
    {
        private readonly RedeSocialContext _context;

        public AmigosRepository(RedeSocialContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(AmigosModel amigosModel)
        {
            await _context.AddAsync(amigosModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AmigosModel amigosModel)
        {
            _context.Update(amigosModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var amigosModel = await _context.AmigosModel.FindAsync(id);

            _context.AmigosModel.Remove(amigosModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AmigosModel>> GetAllAsync()
        {
            return await _context.AmigosModel.ToListAsync();
        }

        public async Task<AmigosModel> GetByUsersAsync(string userLogado, string perfilAcessado)
        {
            try
            {
                var consulta = await _context.AmigosModel.FirstOrDefaultAsync(x =>
                    x.UserIdSolicitado == userLogado && x.UserIdSolicitante == perfilAcessado || x.UserIdSolicitado == perfilAcessado && x.UserIdSolicitante == userLogado);
                return consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<AmigosModel>> GetListByUserAsync(string userLogado)
        {
            try
            {
                var consulta = await _context.AmigosModel.Where(x =>
                    x.UserIdSolicitado == userLogado && x.StatusAmizade == 2 || x.UserIdSolicitante == userLogado && x.StatusAmizade == 2).ToListAsync();

                return consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<AmigosModel>> GetSolicitacoesPendentes(string userLogado)
        {
            return await _context.AmigosModel.Where(x =>
                    x.UserIdSolicitado == userLogado && x.StatusAmizade == 1)
                .ToListAsync();
        }

        public async Task<AmigosModel> GetByIdAsync(int id)
        {
            return await _context.AmigosModel.FindAsync(id);
        }
    }
}

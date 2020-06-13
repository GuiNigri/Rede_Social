using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Data.RedeSocial.Context;
using RedeSocial.Model.UoW;

namespace RedeSocial.Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RedeSocialContext _redeSocialContext;
        private bool _disposed;

        public UnitOfWork(
            RedeSocialContext redeSocialContext)
        {
            _redeSocialContext = redeSocialContext;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public async Task CommitAsync()
        {
            await _redeSocialContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _redeSocialContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

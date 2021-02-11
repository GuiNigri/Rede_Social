using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.UoW
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task CommitAsync();
    }
}

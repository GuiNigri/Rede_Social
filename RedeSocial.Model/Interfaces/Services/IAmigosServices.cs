using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface IAmigosServices : IBaseServices<AmigosModel>, IAmigosRepository
    {
    }
}

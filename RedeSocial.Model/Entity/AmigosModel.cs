using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class AmigosModel : BaseModel
    {
        public string UserIdSolicitado { get; set; }
        public string UserIdSolicitante { get; set; }
        public int StatusAmizade { get; set; }
        public DateTime DataInicioAmizade { get; set; }

    }
}

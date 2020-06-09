using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Entity
{
    public class AmigosModel
    {
        public int Id { get; set; }
        public string UserId1 { get; set; }
        public string UserId2 { get; set; }
        public int StatusAmizade { get; set; }
        public DateTime DataInicioAmizade { get; set; }

    }
}

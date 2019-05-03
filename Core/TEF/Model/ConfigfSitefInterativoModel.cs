using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TEF.Model
{
    public class ConfigfSitefInterativoModel
    {
        public string IpSiTef { get; set; }
        public string IdLoja { get; set; }
        public string IdTerminal { get; set; }
        public string Reservado { get; set; }

        public ConfigfSitefInterativoModel(string ipSitef, string idLoja, string idTerminal)
        {
            this.IpSiTef = ipSitef;
            this.IdLoja = idLoja;
            this.IdTerminal = idTerminal;
            this.Reservado = "0";
        }
    }
}

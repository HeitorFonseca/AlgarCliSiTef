using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TEF.Commands
{
    public class EndPaymentCommand
    {

        public int Confirma { get; set; }
        public string CupomFiscal { get; set; }
        public string DataFiscal { get; set; }
        public string HoraFiscal { get; set; }

    }
}

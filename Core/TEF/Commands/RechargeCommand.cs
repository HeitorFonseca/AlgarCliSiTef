using Core.TEF.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.TEF.Commands
{
    public class RechargeCommand
    {
        public int Id { get; set; }
        public string IdLoja { get; set; }
        public string TerminalId { get; set; }    
        public string Valor { get; set; }

        public string Operator { get; set; }

        public PaymentMode PaymentMode { get; set; }
        public string TimezoneId { get; set; }
    }
}

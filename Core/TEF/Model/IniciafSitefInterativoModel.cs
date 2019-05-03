using Core.TEF.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TEF.Model
{
    class IniciafSitefInterativoModel
    {
        public StringBuilder Valor { get; set; }
        public StringBuilder CupomFiscal { get; set; }
        public StringBuilder DataFiscal { get; set; }
        public StringBuilder HoraFiscal { get; set; }
        public StringBuilder Operador { get; set; }
        public PaymentMode PaymentMode { get; set; }

        public IniciafSitefInterativoModel(string valor, string Operator, string cupomFiscal, PaymentMode paymentMode)
        {
            DateTime now = DateTime.Now.ToLocalTime();

            var year = now.Year;
            var month = (now.Month < 10 ? '0' + now.Month.ToString() : now.Month.ToString());
            var day = (now.Day < 10 ? '0' + now.Day.ToString() : now.Day.ToString());

            var hour = (now.Hour < 10 ? '0' + now.Hour.ToString() : now.Hour.ToString());
            var minute = (now.Minute < 10 ? '0' + now.Minute.ToString() : now.Minute.ToString());
            var second = (now.Second < 10 ? '0' + now.Second.ToString() : now.Second.ToString());

            Valor = new StringBuilder(valor);
            CupomFiscal = new StringBuilder(cupomFiscal);
            DataFiscal = new StringBuilder(year + month + day);
            HoraFiscal = new StringBuilder(hour + minute + second);
            Operador = new StringBuilder(Operator);
            PaymentMode = paymentMode;
        }
    }
}

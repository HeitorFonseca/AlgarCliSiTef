using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.TEF.Service
{
    public static class CliSiTefMethods
    {
        [DllImport("CliSiTef64I.dll")]
        public static extern int ConfiguraIntSiTefInterativo(string IPSiTef, string IdLoja, string IdTerminal, string Reservado);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern int IniciaFuncaoSiTefInterativo(int Funcao, StringBuilder Valor, StringBuilder CupomFiscal,
            StringBuilder DataFiscal, StringBuilder HoraFiscal, StringBuilder Operador, StringBuilder ParamAdic);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern int ContinuaFuncaoSiTefInterativo(ref int Comando, ref int TipoCampo, ref int TamMinimo, ref int TamMaximo, StringBuilder Buffr, int TamBuffer, int Continua);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern void FinalizaFuncaoSiTefInterativo(int Confirma, StringBuilder CupomFiscal, StringBuilder DataFiscal, StringBuilder HoraFisca, StringBuilder ParamAdic);
        [DllImport("CliSiTef64I.dll")]
        public static extern int KeepAlivePinPad();


        /* **************************************** */
        [DllImport("CliSiTef64I.dll")]
        public static extern void ConfiguraIntSiTefInterativoA(StringBuilder resultado, string IPSiTef, string IdLoja, string IdTerminal, string Reservado);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern void IniciaFuncaoSiTefInterativoA(StringBuilder resultado, int Funcao, StringBuilder Valor, StringBuilder CupomFiscal,
            StringBuilder DataFiscal, StringBuilder HoraFiscal, StringBuilder Operador, StringBuilder ParamAdic);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern void ContinuaFuncaoSiTefInterativoA(StringBuilder resultado, ref int Comando, ref int TipoCampo, ref int TamMinimo, ref int TamMaximo, StringBuilder Buffr, int TamBuffer, int Continua);

        [DllImport("CliSiTef64I.dll", CharSet = CharSet.Ansi)]
        public static extern void FinalizaFuncaoSiTefInterativoA(StringBuilder Confirma, StringBuilder CupomFiscal, StringBuilder DataFiscal, StringBuilder HoraFisca, StringBuilder ParamAdic);

    }
}

﻿using System;
using System.Text;
using Core.Exceptions;
using Core.Infraestructure.PushNotification;
using Core.Messages;
using Core.TEF.Commands;
using Core.TEF.Enumeration;
using Core.TEF.Model;
using Core.TEF.ViewModels;
using Core.Utils;

namespace Core.TEF.Service
{
    public class TEFService
    {
        private int value;
        private const int CONTINUA = 10000;
        private const int FINALIZA_TRANSACAO = 0;
        private string codBandeira;

        public RechargeViewModel RealizaTransacao(RechargeCommand rechargeCommand)
        {
        
        bool endOfPayment = false;

            /* ******************* Continua Sitef Interativo Variaveis ******************* */
            int Comando = 0;
            int TamBuffer = 0;
            int Continua = 0;
            int TamMinimo = 0;
            int TamMaximo = 0;
            int TipoCampo = 0;
            StringBuilder Buffer = new StringBuilder(20000); //buffer de 20k bytes  
            /* *************************************************************************** */

            IniciafSitefInterativoModel sitefInterativo = new IniciafSitefInterativoModel(rechargeCommand.Valor, rechargeCommand.Operator, "123456", rechargeCommand.PaymentMode);

            try
            {

                CheckPinPad();

                value = CliSiTefMethods.IniciaFuncaoSiTefInterativo((int)sitefInterativo.PaymentMode, sitefInterativo.Valor, sitefInterativo.CupomFiscal, sitefInterativo.DataFiscal, sitefInterativo.HoraFiscal, sitefInterativo.Operador, null);

                if (value != CONTINUA)
                {
                    TefMessages.ValueErrors(value);
                }

                while (!endOfPayment)
                {

                    value = CliSiTefMethods.ContinuaFuncaoSiTefInterativo(ref Comando, ref TipoCampo, ref TamMinimo, ref TamMaximo, Buffer, TamBuffer, Continua);

                    if (value == CONTINUA)
                    {
                        ComandoTipoCampo(Comando, TipoCampo, ref Buffer, rechargeCommand.Id);
                    }
                    else if (value == FINALIZA_TRANSACAO)
                    {
                        endOfPayment = true;
                    }
                    else
                    {
                        TefMessages.ValueErrors(value);
                    }  
                    
                }
            } catch (Exception e)
            {
                // Se foi lançada nenhuma exceção é porque ocorreu erro na transação
                CliSiTefMethods.FinalizaFuncaoSiTefInterativo(0, sitefInterativo.CupomFiscal, sitefInterativo.DataFiscal, sitefInterativo.HoraFiscal, null);

                throw e;
            }

            return new RechargeViewModel()
            {
                CupomFiscal = sitefInterativo.CupomFiscal.ToString(),
                HoraFiscal = sitefInterativo.HoraFiscal.ToString(),
                DataFiscal = sitefInterativo.DataFiscal.ToString(),
                Bandeira = codBandeira
            };
        }

        public void FinalizaPagamento (EndPaymentCommand endPaymentCommand)
        {
            try
            {
                CheckPinPad();

                // Se não foi lançada nenhuma exceção é porque a transação foi finalizada com sucesso
                CliSiTefMethods.FinalizaFuncaoSiTefInterativo(1, new StringBuilder(endPaymentCommand.CupomFiscal), new StringBuilder(endPaymentCommand.DataFiscal), new StringBuilder(endPaymentCommand.HoraFiscal), null);
            }
            catch(Exception e) { throw e; }
        }

        private void ComandoTipoCampo(int comando, int tipoCampo, ref StringBuilder buffer, int terminalId)
        {
            Object obj;

            switch (comando)
            {

                case 0:

                    if (tipoCampo == 121)
                    {
                        obj = new
                        {
                            code = tipoCampo,
                            message = buffer.ToString()
                        };

                        PushServiceBuilder.GetInstance().Trigger(new[] { $"terminal-{terminalId}" }, "PinPadMessageChanged", obj);
                    }
                    else if (tipoCampo == 132)
                    {
                        this.codBandeira = GetCodBandeira(buffer.ToString());
                    }

                    break;

                case 3:

                    obj = new
                    {
                        code = tipoCampo,
                        message = buffer.ToString()
                    };

                    PushServiceBuilder.GetInstance().Trigger(new[] { $"terminal-{terminalId}" }, "PinPadMessageChanged", obj);

                    break;

                case 4: 

                    if (buffer.ToString() == "Selecione a forma de pagamento")
                        buffer = new StringBuilder("1"); /* Pagamento somente À vista */

                    break;

                case 20:
                    if (buffer.ToString() == "13 - Operacao Cancelada?")
                        buffer = new StringBuilder("0"); /* Cancelar pagamento ao clicar em anula na maquina */
                    break;
                
            }
        }
        
        private string GetCodBandeira(string codBandeira)
        {
            string bandeira = String.Empty;

            switch (codBandeira)
            {
                case "00001":   // VISA CREDITO
                    bandeira = "VISA";
                    break;

                case "20001": //  Maestro Débito 
                    bandeira = "MAESTRO";
                    break;

                case "00002": // Mastercard Crédito
                    bandeira = "MASTERCARD";
                    break;

                case "20002": // Visa Electron Débito
                    bandeira = "VISA ELECTRON";
                    break;

            }

            return bandeira;
        }

        private void CheckPinPad()
        {
            var keepAlive = CliSiTefMethods.KeepAlivePinPad();
            TefMessages.PinpadErrors(keepAlive);
        }

    }
}

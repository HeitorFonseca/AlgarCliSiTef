using System;
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
        private bool insertCard = false;
        private DateTime startTime;

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

                        if (Comando == 0 && TipoCampo == 132 && string.IsNullOrEmpty(codBandeira))
                        {
                            Continua = -1;
                        }
                    }
                    else if (value == FINALIZA_TRANSACAO)
                    {
                        endOfPayment = true;
                    }
                    else
                    {
                        TefMessages.ValueErrors(value, Continua);
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

                        TerminalMessages.SendMessage(terminalId, obj);
                    }
                    else if (tipoCampo == 132)
                    {
                        codBandeira = VerificaCodBandeira(buffer.ToString());
                    }

                    insertCard = false;
                    break;

                case 3:

                    obj = new
                    {
                        code = tipoCampo,
                        message = buffer.ToString() == "70 - Modo Invalido. Retire e Passe o Cartao" ? "Modo de pagamento não suportado, somente crédito. Retire o Cartão" : buffer.ToString()
                    };

                    TerminalMessages.SendMessage(terminalId, obj);

                    insertCard = false;
                    break;

                case 20:
                    if (buffer.ToString() == "13 - Operacao Cancelada?")
                        buffer = new StringBuilder("0"); /* Cancelar pagamento ao clicar em anula na maquina */

                    insertCard = false;
                    break;

                case 22:

                    obj = new
                    {
                        code = tipoCampo,
                        message = (buffer.ToString() == "Cartao nao configurado" ? "Cartão não suportado, somente Visa e Master" : buffer.ToString())
                    };

                    TerminalMessages.SendMessage(terminalId, obj);

                    break;

                case 23:
                    if (tipoCampo == -1)
                    {
                        if (!insertCard)
                        {
                            startTime = DateTime.Now;
                            insertCard = true;
                        }
                        else
                        {
                            if (DateTime.Now.Subtract(startTime).TotalMilliseconds > 60000)
                                TefMessages.TimeoutErrors();
                        }
                    }
                                                         
                    break;

                default:
                    insertCard = false;
                    break;
            }
        }
        
        private string VerificaCodBandeira(string codBandeira)
        {
            string bandeira = String.Empty;

            switch (codBandeira)
            {
                case "00001":   // VISA CREDITO
                    bandeira = "VISA";
                    break;

                case "00002": // Mastercard Crédito
                    bandeira = "MASTERCARD";
                    break;

                default:    // Só serão aceitos os cartões VISA e MASTER
                    throw new BusinessException(BusinessMessages.Error.BRAND_ERROR);
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

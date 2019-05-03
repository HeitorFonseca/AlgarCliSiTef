using Core.Exceptions;
using Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.Utils
{
    public static class TefMessages
    {
        public static void ConfigErrors(int value)
        {
            switch (value)
            {
                case 1:
                    throw new BusinessException(BusinessMessages.ConfigError.ADDRESS_TCP_IP_ERROR);

                case 2:
                    throw new BusinessException(BusinessMessages.ConfigError.INVALID_STORE);

                case 3:
                    throw new BusinessException(BusinessMessages.ConfigError.INVALID_TERMINAL);

                case 6:
                    throw new BusinessException(BusinessMessages.ConfigError.INIT_TCP_IP_ERROR);

                case 7:
                    throw new BusinessException(BusinessMessages.ConfigError.MEMORY_ERROR);

                case 8:
                    throw new BusinessException(BusinessMessages.ConfigError.NOT_FIND_CLISITEF_ERROR);

                case 9:
                    throw new BusinessException(BusinessMessages.ConfigError.EXCEDED_CONF_SERVER_ERROR);

                case 10:
                    throw new BusinessException(BusinessMessages.ConfigError.CLISITEF_ACCESS_FOLDER_ERROR);

                case 11:
                    throw new BusinessException(BusinessMessages.ConfigError.INVALID_DATA_ERROR);

                case 12:
                    throw new BusinessException(BusinessMessages.ConfigError.SAFE_MODE_ERROR);

                case 13:
                    throw new BusinessException(BusinessMessages.ConfigError.INVALID_DLL_PATH_ERROR);

                default:
                    throw new BusinessException(BusinessMessages.Error.ERROR);

            }
        }

        public static void ValueErrors(int value)
        {
            switch (value)
            {
                case -2:
                    throw new BusinessException(BusinessMessages.InitError.OPERATOR_CANCEL_ERROR);

                case -5:
                    throw new BusinessException(BusinessMessages.InitError.SITEF_COMMUNICATION_ERROR);

                case -6:
                    throw new BusinessException(BusinessMessages.InitError.PINPAD_CANCEL_ERROR);

                case -15:
                    throw new BusinessException(BusinessMessages.InitError.AUTOM_CANCEL_ERROR);

                default:
                    throw new BusinessException(BusinessMessages.Error.ERROR);

            }
        }

        public static void PinpadErrors(int value)
        {
            if (value == 0)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------");
                Console.WriteLine("|------------------------- PinPad Nâo Conectado ---------------------------------------|");
                Console.WriteLine("----------------------------------------------------------------------------------------\n\n\n\n\n");
                Console.WriteLine("CTRL + C para sair");

                WaitXSeconds(5);

                throw new BusinessException(BusinessMessages.PinPadError.NO_PINPAD_FOUND_ERROR);
            }
            else if (value == -1)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------");
                Console.WriteLine("|------------- biblioteca de acesso ao PinPad não encontrada ---------------------------|");
                Console.WriteLine("----------------------------------------------------------------------------------------\n\n\n\n\n");
                Console.WriteLine("CTRL + C para sair");

                WaitXSeconds(5);

                throw new BusinessException(BusinessMessages.PinPadError.NO_PINPAD_LIB_FOUND_ERROR);
            }
        }

        private static void WaitXSeconds(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
        }

    }
}

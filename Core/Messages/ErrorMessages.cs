using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages
{
    public partial class BusinessMessages
    {

        public static class ConfigError
        {

            public const string ADDRESS_TCP_IP_ERROR_CODE = "1";
            public static readonly Message ADDRESS_TCP_IP_ERROR = new ErrorMessage
            {
                Code = ADDRESS_TCP_IP_ERROR_CODE,
                Description = "Código de terminal inválido."
            };
    
            public const string INVALID_STORE_CODE = "2";
            public static readonly Message INVALID_STORE = new ErrorMessage
            {
                Code = INVALID_STORE_CODE,
                Description = "Código da loja inválido."
            };


            public const string INVALID_TERMINAL_CODE = "3";
            public static readonly Message INVALID_TERMINAL = new ErrorMessage
            {
                Code = INVALID_TERMINAL_CODE,
                Description = "Código de terminal inválido."
            };

            public const string INIT_TCP_IP_ERROR_CODE = "6";
            public static readonly Message INIT_TCP_IP_ERROR = new ErrorMessage
            {
                Code = INIT_TCP_IP_ERROR_CODE,
                Description = "Código de terminal inválido."
            };
        
            public const string MEMORY_ERROR_CODE = "7";
            public static readonly Message MEMORY_ERROR = new ErrorMessage
            {
                Code = MEMORY_ERROR_CODE,
                Description = "Falta de memória"
            };

            public const string NOT_FIND_CLISITEF_ERROR_CODE = "8";
            public static readonly Message NOT_FIND_CLISITEF_ERROR = new ErrorMessage
            {
                Code = NOT_FIND_CLISITEF_ERROR_CODE,
                Description = "Não encontrou a CliSiTef ou ela está com problemas."
            };

            public const string EXCEDED_CONF_SERVER_ERROR_CODE = "9";
            public static readonly Message EXCEDED_CONF_SERVER_ERROR = new ErrorMessage
            {
                Code = EXCEDED_CONF_SERVER_ERROR_CODE,
                Description = "Configuração de servidores SiTef foi excedida."
            };

            public const string CLISITEF_ACCESS_FOLDER_ERROR_CODE = "10";
            public static readonly Message CLISITEF_ACCESS_FOLDER_ERROR = new ErrorMessage
            {
                Code = CLISITEF_ACCESS_FOLDER_ERROR_CODE,
                Description = "Erro de acesso na pasta CliSiTef (possível falta de permissão para escrita)."
            };

            public const string INVALID_DATA_ERROR_CODE = "11";
            public static readonly Message INVALID_DATA_ERROR = new ErrorMessage
            {
                Code = INVALID_DATA_ERROR_CODE,
                Description = "Dados inválidos passados pela automação."
            };

            public const string SAFE_MODE_ERROR_CODE = "12";
            public static readonly Message SAFE_MODE_ERROR = new ErrorMessage
            {
                Code = SAFE_MODE_ERROR_CODE,
                Description = "Modo seguro não ativo (possível falta de configuração no servidor SiTef do arquivo .cha)."
            };

            public const string INVALID_DLL_PATH_ERROR_CODE = "13";
            public static readonly Message INVALID_DLL_PATH_ERROR = new ErrorMessage
            {
                Code = INVALID_DLL_PATH_ERROR_CODE,
                Description = "Caminho DLL inválido (o caminho completo das bibliotecas está muito grande)."
            };

        }

        public static class InitError
        {

            public const string OPERATOR_CANCEL_ERROR_CODE = "-2";
            public static readonly Message OPERATOR_CANCEL_ERROR = new ErrorMessage
            {
                Code = OPERATOR_CANCEL_ERROR_CODE,
                Description = "Operação cancelada pelo operador."
            };

            public const string SITEF_COMMUNICATION_ERROR_CODE = "-5";
            public static readonly Message SITEF_COMMUNICATION_ERROR = new ErrorMessage
            {
                Code = SITEF_COMMUNICATION_ERROR_CODE,
                Description = "Sem comunicação com o SiTef."
            };


            public const string PINPAD_CANCEL_ERROR_CODE = "-6";
            public static readonly Message PINPAD_CANCEL_ERROR = new ErrorMessage
            {
                Code = PINPAD_CANCEL_ERROR_CODE,
                Description = "Operação cancelada pelo usuário (no pinpad)."
            };

            public const string AUTOM_CANCEL_ERROR_CODE = "-15";
            public static readonly Message AUTOM_CANCEL_ERROR = new ErrorMessage
            {
                Code = AUTOM_CANCEL_ERROR_CODE,
                Description = "Operação cancelada pela automação comercial."
            };

        }

        public static class PinPadError
        {
            public const string NO_PINPAD_FOUND_ERROR_CODE = "0";
            public static readonly Message NO_PINPAD_FOUND_ERROR = new ErrorMessage
            {
                Code = NO_PINPAD_FOUND_ERROR_CODE,
                Description = "Não existe um PinPad conectado ao micro"
            };

            public const string NO_PINPAD_LIB_FOUND_ERROR_CODE = "-1";
            public static readonly Message NO_PINPAD_LIB_FOUND_ERROR = new ErrorMessage
            {
                Code = NO_PINPAD_LIB_FOUND_ERROR_CODE,
                Description = "biblioteca de acesso ao PinPad não encontrada"
            };
        }

        public static class Error
        {

            public const string ERROR_CODE = "9999";
            public static readonly Message ERROR = new ErrorMessage
            {
                Code = ERROR_CODE,
                Description = "Erro inexperado ocorreu."
            };

            public const string TIMEOUT_ERROR_CODE = "9998";
            public static readonly Message TIMEOUT_ERROR = new ErrorMessage
            {
                Code = ERROR_CODE,
                Description = "Tempo de operação de recarga expirado."
            };

            public const string BRAND_ERROR_CODE = "9997";
            public static readonly Message BRAND_ERROR = new ErrorMessage
            {
                Code = ERROR_CODE,
                Description = "Bandeira não suportada, somente Visa e Master."
            };

            public const string INSERT_CARD_TIMEOUT_ERROR_CODE = "9996";
            public static readonly Message INSERT_CARD_TIMEOUT_ERROR = new ErrorMessage
            {
                Code = ERROR_CODE,
                Description = "Tempo de espera expirado"
            };
        }
    }
}

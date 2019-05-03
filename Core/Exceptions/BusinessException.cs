using Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class BusinessException : Exception
    {
        public Message Message { get; set; }
        public object Parameters { get; set; }

        public BusinessException(Message message)
        {
            this.Message = message;
        }

        public BusinessException(Message message, object parameters)
            : this(message)
        {
            this.Parameters = parameters;
        }
    }
}

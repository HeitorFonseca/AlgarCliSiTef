using Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgarCliSiTef.http
{
    class ResponseFilterMessage
    {
        public string Type { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public object Parameters { get; private set; }

        public ResponseFilterMessage(Message message)
        {
            this.Type = message.Type.ToString();
            this.Code = message.Code;
            this.Description = message.Description;
            this.Parameters = new object();
        }

        public ResponseFilterMessage(Message message, object parameters) : this(message)
        {
            if (parameters != null)
            {
                this.Parameters = parameters;
            }
        }
    }
}

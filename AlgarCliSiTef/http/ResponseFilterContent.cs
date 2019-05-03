using Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgarCliSiTef.http
{
    class ResponseFilterContent
    {
        public int Status { get; private set; }
        public ICollection<ResponseFilterMessage> Messages { get; private set; }
        public dynamic ResponseData { get; private set; }

        public ResponseFilterContent()
        {
            Status = 200;
            Messages = new List<ResponseFilterMessage>();
        }

        public ResponseFilterContent(Message message) : this()
        {
            AddMessage(message);
        }

        public ResponseFilterContent(Message message, object parameters)
            : this()
        {
            AddMessage(message, parameters);
        }

        public ResponseFilterContent(Message message, object parameters, object response)
            : this()
        {
            AddMessage(message, parameters);
            ResponseData = response;
        }

        public void AddMessage(Message message)
        {
            Messages.Add(new ResponseFilterMessage(message));
        }

        public void AddMessage(Message message, object parameters)
        {
            Messages.Add(new ResponseFilterMessage(message, parameters));
        }
    }
}

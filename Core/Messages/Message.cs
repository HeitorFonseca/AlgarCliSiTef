using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Messages
{
    public class Message
    {
        public Message(MessageType type)
        {
            this.Type = type;
        }

        public MessageType Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public static Message GetMessage(string code)
        {
            foreach (Message message in GetMessages())
            {
                if (message.Code == code)
                {
                    return message;
                }
            }
            return null;
        }

        private static List<Message> GetMessages()
        {
            List<Message> messages = new List<Message>();
            Type[] types = { typeof(BusinessMessages.Info), typeof(BusinessMessages.Error), typeof(BusinessMessages.ConfigError), typeof(BusinessMessages.InitError), typeof(BusinessMessages.Success) };
            foreach (Type type in types)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (FieldInfo field in fields)
                {
                    object value = field.GetValue(type);
                    if (value is Message)
                    {
                        messages.Add((Message)value);
                    }
                }
            }
            return messages;
        }
    }

    public class InfoMessage : Message
    {
        public InfoMessage() : base(MessageType.INFO) { }
    }
    public class ErrorMessage : Message
    {
        public ErrorMessage() : base(MessageType.ERROR) { }
    }
    public class SuccessMessage : Message
    {
        public SuccessMessage() : base(MessageType.SUCCESS) { }
    }
}

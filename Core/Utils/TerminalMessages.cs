using Core.Infraestructure.PushNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utils
{
    public static class TerminalMessages
    {
        public static void SendMessage(int terminalId, Object obj) 
        {
            PushServiceBuilder.GetInstance().Trigger(new[] { $"terminal-{terminalId}" }, "PinPadMessageChanged", obj);
        }
    }
}

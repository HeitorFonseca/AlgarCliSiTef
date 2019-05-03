using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infraestructure.PushNotification
{
    public interface IPushService
    {
        void Trigger(string[] channels, string eventName, object paramData);
    }
}

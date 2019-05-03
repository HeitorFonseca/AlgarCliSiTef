using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infraestructure.PushNotification
{
    public class PushServiceBuilder
    {
        public static IPushService GetInstance()
        {
            return new PusherClientService();
        }
    }
}

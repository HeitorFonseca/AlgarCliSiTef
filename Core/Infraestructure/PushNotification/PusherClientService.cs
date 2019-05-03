using PusherServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;

namespace Core.Infraestructure.PushNotification
{
    class PusherClientService : IPushService
    {
        private readonly IPusher _pusher;

        internal PusherClientService()
        {
            var options = new PusherOptions { JsonSerializer = new CustomerSerializer() };
            //_pusher = new Pusher(
            //    ConfigurationManager.AppSettings.Get("pusher-id"),
            //    ConfigurationManager.AppSettings.Get("pusher-key"),
            //    ConfigurationManager.AppSettings.Get("pusher-secret"),
            //    options);

            _pusher = new Pusher("75465", "0041ab94e110de594bdb", "965b8dd2bd7f6cec18cc", options);

        }

        internal class CustomerSerializer : ISerializeObjectsToJson
        {
            public string Serialize(object objectToSerialize)
            {
                return JsonConvert.SerializeObject(objectToSerialize, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }

        public void Trigger(string[] channels, string eventName, object paramData)
        {
            _pusher.TriggerAsync(channels, eventName, paramData);
        }
    }
}

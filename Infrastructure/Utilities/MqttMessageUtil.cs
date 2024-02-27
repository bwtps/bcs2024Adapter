using MQTTnet.Protocol;
using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extension;

namespace Infrastructure.Utilities
{
    public class MqttMessageUtil
    {
        private const string TopicFormatWithUri = "{0}/type/{1}/uri/{2}/{3}";
        //public static MqttApplicationMessage ToIotMqttMessage<T>(string deviceType, string uri, string feature, T data, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce) where T : class
        //{
        //    var builder = new MqttApplicationMessageBuilder().WithTopic(string.Format(TopicFormatWithUri, "iot/down", deviceType, uri,
        //            feature));
        //    var json = JsonConvert.SerializeObject(data, JsonExtensions.SerializerSettings);
        //    var mqttMessage = builder
        //        .WithPayload(json)
        //        .WithQualityOfServiceLevel(qos)
        //        .Build();
        //    return mqttMessage;
        //}

        public static MqttApplicationMessage ToIotMqttMessage(string flow,string deviceType, string uri, string feature, byte[] PayloadBuffer, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce)
        {
            var builder = new MqttApplicationMessageBuilder().WithTopic(string.Format(TopicFormatWithUri, flow, deviceType, uri,
                    feature));

            var mqttMessage = builder
                .WithPayload(PayloadBuffer)
                .WithQualityOfServiceLevel(qos)
                .Build();
            return mqttMessage;
        }
    }
}

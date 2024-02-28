using Autofac.Core;
using Domain.Adapter;
using Domain.Adapter.Mqtt;
using Domain.Adapter.State;
using Infrastructure.Utilities;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Adapter
{
    public class MqttWorker : BackgroundService
    {
        private readonly ILogger<MqttWorker> _logger;
        public IotMqttTool IotMqttTool { get; init; } = null!;
        //public IotMqttPushTool IotMqttPushTool { get; init; } = null!;
        public MqttWorker(ILogger<MqttWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //await IotMqttPushTool.StartAsync();
                await IotMqttTool.StartAsync(ReceiveCallbackAsync);
            }
            catch (Exception ex)
            {
                _logger.LogError("MqttWorker Exception:" + ex);
            }
        }


        public async Task<IotMsg> ReceiveCallbackAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            IotMsg? msg = null;
            string topic = arg.ApplicationMessage.Topic;
            var payloadSegment = arg.ApplicationMessage.PayloadSegment;
            Regex topicRegex = new(@"^iot\/up\/type\/(?<type>\d+)\/uri\/(?<uri>[^\/]+)\/(?<feat>[^\/]+)$");
            var match = topicRegex.Match(topic);
            if (match.Success)
            {

                string type = match.Groups["type"].Value;
                string uri = match.Groups["uri"].Value;
                string feat = match.Groups["feat"].Value;

                if (feat == "calibration")//校验
                {
                    await CalibrationHandler(type, uri, feat);
                }
                else if (feat == "state")//上下线
                {
                    await StateHandler(type, uri, feat, payloadSegment.ToArray());
                }
                else if (feat == "health")//设备健康
                {
                    await HealthHandler(type, uri, feat, payloadSegment.ToArray());
                }
                else if (feat == "data")//实时数据
                {
                    await DataHandler(type, uri, feat, payloadSegment.ToArray());
                    //var payload = channelData.ApplicationMessage.Payload;
                    //await service.State(type, uri, feat, channelData.ApplicationMessage.Payload);
                }
            }

            return msg;
        }

        public async Task CalibrationHandler(string type, string uri, string feat)
        {
            var calibration = new IotMqttCalibration();
            MqttApplicationMessage message = MqttMessageUtil.ToIotMqttMessage("iot/down", type.ToString(), uri, feat, calibration.ConvertToHex(), MqttQualityOfServiceLevel.ExactlyOnce);
            await IotMqttTool.PublishAsync(message);
            message = MqttMessageUtil.ToIotMqttMessage("iot/down", type.ToString(), uri, feat, Encoding.UTF8.GetBytes(calibration.ConvertToJson()), MqttQualityOfServiceLevel.ExactlyOnce);
            await IotMqttTool.PublishAsync(message);
            //var calibration = new IotMqttCalibration();
            //MqttApplicationMessage message = MqttMessageUtil.ToIotMqttMessage("adapter/down", type.ToString(), uri, feat, calibration.ConvertToHex(), MqttQualityOfServiceLevel.ExactlyOnce);
            //await IotMqttTool.PublishAsync(message);
            //message = MqttMessageUtil.ToIotMqttMessage("adapter/down", type.ToString(), uri, feat, Encoding.UTF8.GetBytes(calibration.ConvertToJson()), MqttQualityOfServiceLevel.ExactlyOnce);
            //await IotMqttTool.PublishAsync(message);
        }

        public async Task StateHandler(string type, string uri, string feat, byte[] data)
        {
            var state = new IotMqttState(data);
            await IotMqttTool.PublishAsync(MqttMessageUtil.ToIotMqttMessage("adapter/up", type, uri, feat, Encoding.UTF8.GetBytes(state.ConvertToJson()), MqttQualityOfServiceLevel.ExactlyOnce));
        }
        public async Task HealthHandler(string type, string uri, string feat, byte[] data)
        {
            var health = new IotMqttHealth(data, type);
            await IotMqttTool.PublishAsync(MqttMessageUtil.ToIotMqttMessage("adapter/up", type, uri, feat, Encoding.UTF8.GetBytes(health.ConvertToJson()), MqttQualityOfServiceLevel.AtMostOnce));
        }
        public async Task DataHandler(string type, string uri, string feat, byte[] data)
        {
            var state = new IotMqttData(data, type);
            await IotMqttTool.PublishAsync(MqttMessageUtil.ToIotMqttMessage("adapter/up", type, uri, feat, Encoding.UTF8.GetBytes(state.ConvertToJson()), MqttQualityOfServiceLevel.AtMostOnce));
        }
    }
}

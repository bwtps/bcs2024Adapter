using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public class IotMqttPushTool
    {
        private IManagedMqttClient _mqttClient;
        private ManagedMqttClientOptions _clientOptions;
        public IotMqttPushTool(IConfiguration configuration)
        {
            var mqtt = configuration.GetConnectionString("bcs_mqtt")!.Split(":");
            var optionsBuilder = new ManagedMqttClientOptionsBuilder().WithAutoReconnectDelay(TimeSpan.FromSeconds(10)).WithClientOptions(new MqttClientOptionsBuilder()
            .WithTcpServer(mqtt[0], Convert.ToInt32(mqtt[1])).WithCredentials(mqtt[2], mqtt[3])
            .WithClientId("clientId-Adapter" + DateTime.Now.ToString("yyyyMMddHHmmss"))
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(10)).WithCleanSession()
            .WithWillTopic("state/system").WithWillPayload(new byte[] { 0x0 })); ; ;

            _clientOptions = optionsBuilder.Build();
            _mqttClient = new MqttFactory().CreateManagedMqttClient();
            _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync; // 客户端连接成功事件
            _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync; // 客户端连接关闭事件
        }

        public async Task StartAsync()
        {
            await _mqttClient.StartAsync(_clientOptions);
        }

        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            var message = new MqttApplicationMessage
            {
                Topic = "state/system",
                PayloadSegment = new byte[] { 0xFF },
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                //Retain = true  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
            };
            await _mqttClient.InternalClient.PublishAsync(message);
        }

        public async Task PublishAsync(MqttApplicationMessage message)
        {
            await _mqttClient.EnqueueAsync(message);
        }
    }
}

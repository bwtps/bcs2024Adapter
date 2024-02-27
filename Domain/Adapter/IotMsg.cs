using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Adapter
{
    public class IotMsg
    {
        public string Topic { get; set; } = null!;

        public MqttQualityOfServiceLevel Qs { get; set; }

        public string JsonStr { get; set; } = null!;
    }
}

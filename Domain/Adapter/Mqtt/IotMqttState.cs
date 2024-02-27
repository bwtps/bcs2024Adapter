using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Mqtt
{
    public class IotMqttState
    {

        public byte[] Data { get; set; }

        public IotMqttState(byte[] data)
        {
            Data = data;
        }
        public string ConvertToJson()
        {
            string str = Encoding.UTF8.GetString(Data);
            return str;
        }

        public string ConvertToIotMsg()
        {
            string str = Encoding.UTF8.GetString(Data);
            return str;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Mqtt
{
    public class IotMqttHealth
    {
        public string DeviceType { get; set; }
        public byte[] Data { get; set; }

        public IotMqttHealth(byte[] data, string deviceType)
        {
            Data = data;
            DeviceType = deviceType;
        }
        public string ConvertToJson()
        {
            if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.IntelligentElectricMedicalBed)
            {

            }
            else if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.SignMonitoringMattress)
            {

            }
            else if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.IntelligentAntiBedsoreMattress)
            {
            }
            string str = Encoding.UTF8.GetString(Data);
            return str;
        }

        //public IotMsg ConvertToIotMsg()
        //{
        //    string str = Encoding.UTF8.GetString(Data);
        //    return str;
        //}
    }
}

using Newtonsoft.Json;
using Utilities;
using Domain.Adapter.Calibration;


namespace Domain.Adapter.Mqtt
{
    public class IotMqttCalibration
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonIgnore]
        public byte[] FrameHeader { get => new byte[] { 0xA5, 0x5A }; }
        [JsonIgnore]
        public int FrameLength { get => 5 + Data.Length; }
        [JsonIgnore]
        public byte FrameType { get; set; } = 0x20;
        [JsonIgnore]
        public byte[] Data { get; set; }

        public IotMqttCalibration()
        {
            Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Data = BitConverter.GetBytes(Time);
        }

        public string ConvertToJson()
        {
            DeviceCalibration calibration = new DeviceCalibration
            {
                Time = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()
            };
            var str = JsonConvert.SerializeObject(calibration);
            return str;
        }
        public byte[] ConvertToHex()
        {
            var payloadCount = 5 + Data.Length;
            var headers = new[] { (byte)payloadCount, (byte)(payloadCount >> 8), FrameType };
            var dataBytes = FrameHeader.Concat(headers).Concat(Data);
            return dataBytes.AppendCrc32().ToArray();
        }
    }
}

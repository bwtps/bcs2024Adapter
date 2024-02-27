using Domain.Adapter.Health;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Adapter.Mqtt
{
    public class IotMqttData
    {
        public byte[] FrameHeader { get => new byte[] { 0xA5, 0x5A }; }
        public string DeviceType { get; set; }
        public byte FrameType { get; set; }
        public byte[] FrameData { get; set; }
        public byte[] Data { get; set; }

        public IotMqttData(byte[] data, string deviceType)
        {
            Data = data;
            FrameType = data[4];
            FrameData = data[5..(data.Length - 4)];
            DeviceType = deviceType;
        }
        public string ConvertToJson()
        {
            var header = Data[0..2];
            var str = "";
            if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.IntelligentElectricMedicalBed)
            {
                var iotdata = new IntelligentElectricMedicalBedData();
                if (header.SequenceEqual(FrameHeader))
                {
                    //16进制
                    var guardrail = FrameData.ElementAtOrDefault(8);
                    var offBed = FrameData.ElementAtOrDefault(10);
                    var angle = FrameData.ElementAtOrDefault(11);
                    var battery = FrameData.ElementAtOrDefault(21);
        
                    iotdata.Time = BitConverter.ToInt64(FrameData.AsSpan()[..8]);
                    iotdata.Guardrail = [guardrail & 0x3, (guardrail >> 2) & 0x3, (guardrail >> 4) & 0x3, (guardrail >> 6) & 0x3];
                    iotdata.OffBed = [offBed & 0x3, (offBed >> 2) & 0x3];
                    iotdata.Brake = [offBed & 0x3];
                    iotdata.Angle = [FrameData.ElementAtOrDefault(12), FrameData.ElementAtOrDefault(13), FrameData.ElementAtOrDefault(14), FrameData.ElementAtOrDefault(15), FrameData.ElementAtOrDefault(16)];
                    iotdata.Height = [FrameData.ElementAtOrDefault(17), FrameData.ElementAtOrDefault(18)];

                    iotdata.Weight = BitConverter.ToInt16(FrameData.AsSpan()[19..21]);
                    iotdata.Battery = battery & 0x7f;
                    iotdata.Charge = (battery >> 7) & 0x1;
                    iotdata.Warn = FrameData.ElementAtOrDefault(22);
                    iotdata.Light = FrameData.ElementAtOrDefault(23);
                }
                else
                {
                    JObject obj = JObject.Parse(Encoding.UTF8.GetString(Data));
                    iotdata.Time = Convert.ToInt64(obj["time"]);
                    iotdata.Guardrail = JsonConvert.DeserializeObject<int[]>(obj["guardrail"]!.ToString())!;
                    iotdata.OffBed = JsonConvert.DeserializeObject<int[]>(obj["offBed"]!.ToString())!;
                    iotdata.Brake = JsonConvert.DeserializeObject<int[]>(obj["brake"]!.ToString())!;
                    iotdata.Angle = JsonConvert.DeserializeObject<int[]>(obj["angle"]!.ToString())!;
                    iotdata.Height = JsonConvert.DeserializeObject<int[]>(obj["height"]!.ToString())!;

                    iotdata.Weight = Convert.ToInt32(obj["weight"]);
                    iotdata.Battery = Convert.ToInt32(obj["battery"]);
                    iotdata.Charge = Convert.ToInt32(obj["charge"]);
                    iotdata.Warn = Convert.ToInt32(obj["warn"]);
                    iotdata.Light = Convert.ToInt32(obj["light"]);
                }
                str = JsonConvert.SerializeObject(iotdata);
            }
            else if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.SignMonitoringMattress)
            {
                var iotdata = new SignMonitoringMattressData();
                if (header.SequenceEqual(FrameHeader))
                {
                    //16进制
                    iotdata.Time = BitConverter.ToInt64(FrameData.AsSpan()[..8]);
                    iotdata.Heart = FrameData.ElementAtOrDefault(8);
                    iotdata.Breath = FrameData.ElementAtOrDefault(9);
                    iotdata.Move = FrameData.ElementAtOrDefault(10);
                    iotdata.State = FrameData.ElementAtOrDefault(11);
                }
                else
                {
                    JObject obj = JObject.Parse(Encoding.UTF8.GetString(Data));
                    //json
                    iotdata.Time = Convert.ToInt64(obj["time"]);
                    iotdata.Heart = Convert.ToInt32(obj["heart"]);
                    iotdata.Breath = Convert.ToInt32(obj["breath"]);
                    iotdata.Move = Convert.ToInt32(obj["move"]);
                    iotdata.State = Convert.ToInt32(obj["state"]);
                }
                str = JsonConvert.SerializeObject(iotdata);
            }
            else if (Convert.ToInt32(DeviceType) == (int)EnumDeviceType.IntelligentAntiBedsoreMattress)
            {
                var iotdata = new IntelligentAntiBedsoreMattressData();
                if (header.SequenceEqual(FrameHeader))
                {
                    //16进制
                    var overpressureWarn = FrameData.ElementAtOrDefault(8);
                    var warn = FrameData.ElementAtOrDefault(24);

                    iotdata.Time = BitConverter.ToInt64(FrameData.AsSpan()[..8]);

                    iotdata.OverpressureWarn = [overpressureWarn & 0x3, (overpressureWarn >> 2) & 0x3, (overpressureWarn >> 4) & 0x3, (overpressureWarn >> 6) & 0x3];
                    iotdata.Bodydata = [FrameData.ElementAtOrDefault(9), FrameData.ElementAtOrDefault(10)];
                    iotdata.Mode = FrameData.ElementAtOrDefault(11);
                    iotdata.PostureInterval = FrameData.ElementAtOrDefault(12);
                    iotdata.TurnoverMode = FrameData.ElementAtOrDefault(13);
                    iotdata.TurnoverInterval= FrameData.ElementAtOrDefault(14);
                    iotdata.PostureRemaining = BitConverter.ToInt16(FrameData.AsSpan()[15..17]);
                    iotdata.TurnoverRemaining = BitConverter.ToInt16(FrameData.AsSpan()[17..19]);
                    iotdata.CurrentAirColumn = FrameData.ElementAtOrDefault(19);
                    iotdata.Pressure = BitConverter.ToInt16(FrameData.AsSpan()[20..22]);
                    iotdata.Occupancy = FrameData.ElementAtOrDefault(22);
                    iotdata.BodyMove = FrameData.ElementAtOrDefault(23);
                    iotdata.Warn = [warn & 0x1, (warn >> 1) & 0x1, (warn >> 3) & 0x1];
                }
                else
                {
                    JObject obj = JObject.Parse(Encoding.UTF8.GetString(Data));

                    iotdata.Time = Convert.ToInt64(obj["time"]);
                    iotdata.OverpressureWarn= JsonConvert.DeserializeObject<int[]>(obj["overpressureWarn"]!.ToString())!;
                    iotdata.Bodydata = JsonConvert.DeserializeObject<int[]>(obj["bodydata"]!.ToString())!;
                    iotdata.Mode = Convert.ToInt32(obj["mode"]);
                    iotdata.PostureInterval = Convert.ToInt32(obj["postureInterval"]);
                    iotdata.TurnoverMode = Convert.ToInt32(obj["turnoverMode"]);
                    iotdata.TurnoverInterval = Convert.ToInt32(obj["turnoverInterval"]);
                    iotdata.PostureRemaining = Convert.ToInt32(obj["postureRemaining"]);
                    iotdata.TurnoverRemaining = Convert.ToInt32(obj["turnoverRemaining"]);
                    iotdata.CurrentAirColumn = Convert.ToInt32(obj["currentAirColumn"]);
                    iotdata.Pressure = Convert.ToInt32(obj["pressure"]);
                    iotdata.Occupancy = Convert.ToInt32(obj["occupancy"]);
                    iotdata.BodyMove = Convert.ToInt32(obj["bodyMove"]);
                    iotdata.Warn =JsonConvert.DeserializeObject<int[]>(obj["warn"]!.ToString())!;
                }
                str = JsonConvert.SerializeObject(iotdata);
            }

            return str;
        }
    }
}

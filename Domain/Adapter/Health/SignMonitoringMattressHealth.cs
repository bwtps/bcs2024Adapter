using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Health
{ /// <summary>
  ///     无感体征监测垫数据10004
  /// </summary>
    public class SignMonitoringMattressHealth
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("health")]
        public int Health { get; set; }
        [JsonProperty("status")]
        public SignMonitoringMattressHealth_Status Status { get; set; } = null!;
    }

    public class SignMonitoringMattressHealth_Status
    {
        [JsonProperty("opticalFibre")]
        public int OpticalFibre { get; set; }
        [JsonProperty("sensorLoad")]
        public int SensorLoad { get; set; }
        [JsonProperty("lightSignal")]
        public int LightSignal { get; set; }
    }
}

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
    public class SignMonitoringMattressData
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("health")]
        public int Heart { get; set; }
        [JsonProperty("breath")]
        public int Breath { get; set; }
        [JsonProperty("move")]
        public int Move { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
    }
}

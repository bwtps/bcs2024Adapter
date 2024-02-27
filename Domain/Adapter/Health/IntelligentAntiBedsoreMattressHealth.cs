using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Health
{ /// <summary>
  ///     智能防褥疮床垫 10005
  /// </summary>
    public class IntelligentAntiBedsoreMattressHealth
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("health")]
        public int Health { get; set; }
        [JsonProperty("status")]
        public IntelligentAntiBedsoreMattressHealth_Status Status { get; set; } = null!;
    }

    public class IntelligentAntiBedsoreMattressHealth_Status
    {
        [JsonProperty("pressureMattress")]
        public int PressureMattress { get; set; }
        [JsonProperty("controlBox")]
        public int ControlBox { get; set; }
        [JsonProperty("mattressBody")]
        public int MattressBody { get; set; }
    }
}

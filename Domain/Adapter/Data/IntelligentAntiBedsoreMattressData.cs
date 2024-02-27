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
    public class IntelligentAntiBedsoreMattressData
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("overpressureWarn")]
        public int[] OverpressureWarn { get; set; } = null!;
        [JsonProperty("bodydata")]
        public int[] Bodydata { get; set; } = null!;

        [JsonProperty("mode")]
        public int Mode { get; set; }
        [JsonProperty("postureInterval")]
        public int PostureInterval { get; set; }

        [JsonProperty("TurnoverMode")]
        public int TurnoverMode { get; set; }
        [JsonProperty("turnoverInterval")]
        public int TurnoverInterval { get; set; }
        [JsonProperty("postureRemaining")]
        public int PostureRemaining { get; set; }
        [JsonProperty("turnoverRemaining")]
        public int TurnoverRemaining { get; set; }
        [JsonProperty("currentAirColumn")]
        public int CurrentAirColumn { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("occupancy")]
        public int Occupancy { get; set; }
        [JsonProperty("bodyMove")]
        public int BodyMove { get; set; }
        [JsonProperty("warn")]
        public int[] Warn { get; set; } = null!;
    }
}


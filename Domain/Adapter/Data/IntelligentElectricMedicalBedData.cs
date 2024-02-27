using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Health
{ /// <summary>
  ///     智能电动病床10003
  /// </summary>
    public class IntelligentElectricMedicalBedData
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("guardrail")]
        public int[] Guardrail { get; set; } = null!;
        [JsonProperty("offBed")]
        public int[] OffBed { get; set; } = null!;
        [JsonProperty("brake")]
        public int[] Brake { get; set; } = null!;
        [JsonProperty("angle")]
        public int[] Angle { get; set; } = null!;
        [JsonProperty("height")]
        public int[] Height { get; set; } = null!;

        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonProperty("battery")]
        public int Battery { get; set; }
        [JsonProperty("charge")]
        public int Charge { get; set; }
        [JsonProperty("warn")]
        public int Warn { get; set; }
        [JsonProperty("light")]
        public int Light { get; set; }
    }
}


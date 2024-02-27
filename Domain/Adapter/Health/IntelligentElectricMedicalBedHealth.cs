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
    public class IntelligentElectricMedicalBedHealth
    {
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("health")]
        public int Health { get; set; }
        [JsonProperty("status")]
        public IntelligentElectricMedicalBedHealth_Status Status { get; set; } = null!;
    }

    public class IntelligentElectricMedicalBedHealth_Status
    {
        [JsonProperty("battery")]
        public int Battery { get; set; }
        [JsonProperty("smps")]
        public int Smps { get; set; }
        [JsonProperty("panels")]
        public IntelligentElectricMedicalBedHealth_Status_Panels Panels { get; set; } = null!;

        [JsonProperty("Sensors")]
        public int[] Sensors { get; set; } = null!;
        [JsonProperty("Motors")]
        public int[] Motors { get; set; } = null!;
    }

    public class IntelligentElectricMedicalBedHealth_Status_Panels
    {
        [JsonProperty("handler")]
        public int Handler { get; set; }
        [JsonProperty("nurse")]
        public int Nurse { get; set; }
        [JsonProperty("sideRail")]
        public int SideRail { get; set; }
    }
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.Calibration
{
    public class DeviceCalibration
    {
        [JsonProperty("time")]
        public string Time { get; set; } = null!;
    }
}

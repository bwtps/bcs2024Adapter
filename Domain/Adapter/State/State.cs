using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapter.State
{
    public class State
    {
        [JsonProperty("connected")]
        public int Connected { get; set; }
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; } = null!;
        [JsonProperty("mac",NullValueHandling = NullValueHandling.Ignore)]
        public string Mac { get; set; } = null!;
    }
}

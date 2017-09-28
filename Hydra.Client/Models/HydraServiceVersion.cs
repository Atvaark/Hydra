using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class HydraServiceVersion
    {
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("MinorVersion")]
        public int MinorVersion { get; set; }
    }
}
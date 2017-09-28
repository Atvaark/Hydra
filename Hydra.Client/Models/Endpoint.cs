using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class Endpoint
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IP")]
        public string IP { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }

        [JsonProperty("Protocol")]
        public int Protocol { get; set; }

        [JsonProperty("IsDefault")]
        public bool IsDefault { get; set; }
    }
}
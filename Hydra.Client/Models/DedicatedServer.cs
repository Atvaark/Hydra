using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DedicatedServer
    {
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }

        [JsonProperty("ServerSignature")]
        public string ServerSignature { get; set; }
    }
}
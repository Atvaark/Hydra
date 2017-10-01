using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DedicatedServerData
    {
        [JsonProperty("DedicatedServer")]
        public DedicatedServer DedicatedServer { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannel2
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("LastReadVersion")]
        public int LastReadVersion { get; set; }
    }
}

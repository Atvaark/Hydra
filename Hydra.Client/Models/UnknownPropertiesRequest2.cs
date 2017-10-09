using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPropertiesRequest2
    {
        [JsonProperty("type")]
        public int type { get; set; }

        [JsonProperty("properties")]
        public UnknownProperty[] properties { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPropertiesRequest1
    {
        [JsonProperty("properties")]
        public UnknownProperty[] properties { get; set; }
    }
}

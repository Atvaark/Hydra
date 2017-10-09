using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownProperty
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
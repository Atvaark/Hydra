using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class EnvironmentLocalizedDescription
    {
        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownUhGenerationRequest
    {
        [JsonProperty("generation")]
        public int generation { get; set; }
    }
}

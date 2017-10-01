using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ContainerRecord
    {
        [JsonProperty("Layout")]
        public int Layout { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Data")]
        public int[] Data { get; set; }
    }
}
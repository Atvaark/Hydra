using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DataCenterPing
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Ping")]
        public int Ping { get; set; }
    }
}
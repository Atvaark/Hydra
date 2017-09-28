using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DataCenterOccupation
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Occupation")]
        public int Occupation { get; set; }
    }
}
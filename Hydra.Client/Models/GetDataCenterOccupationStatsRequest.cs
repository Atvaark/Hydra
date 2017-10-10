using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationStatsRequest
    {
        [JsonProperty("state")]
        public int state { get; set; }

        [JsonProperty("generation")]
        public int generation { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DataCenterOccupationPollingInterval
    {
        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("PollingIntervalInMs")]
        public int PollingIntervalInMs { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class EnvironmentDataCenters
    {
        [JsonProperty("DataCenterId")]
        public string DataCenterId { get; set; }

        [JsonProperty("Occupation")]
        public int Occupation { get; set; }

        [JsonProperty("HideWhenNotAvailable")]
        public bool HideWhenNotAvailable { get; set; }

        [JsonProperty("Endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("Endpoints")]
        public Endpoint[] Endpoints { get; set; }

        [JsonProperty("LocalizedDescription")]
        public EnvironmentLocalizedDescription[] LocalizedDescription { get; set; }
    }
}
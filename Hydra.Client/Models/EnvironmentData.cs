using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class EnvironmentData
    {
        [JsonProperty("Endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("EnvironmentId")]
        public string EnvironmentId { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("LocalizedDescription")]
        public EnvironmentLocalizedDescription[] LocalizedDescription { get; set; }

        [JsonProperty("DataCenters")]
        public EnvironmentDataCenters[] DataCenters { get; set; }

        [JsonProperty("IsUnderMaintenance")]
        public bool IsUnderMaintenance { get; set; }
    }
}
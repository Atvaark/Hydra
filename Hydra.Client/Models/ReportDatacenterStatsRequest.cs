using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ReportDatacenterStatsRequest
    {
        [JsonProperty("playlist")]
        public string playlist { get; set; }

        [JsonProperty("dcPings")]
        public DataCenterPing[] dcPings { get; set; }
    }
}

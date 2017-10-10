using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ReportDatacenterStatsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }
}

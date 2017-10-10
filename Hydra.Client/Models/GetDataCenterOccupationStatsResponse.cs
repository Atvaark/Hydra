using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationStatsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetDataCenterOccupationData data { get; set; }
    }
}
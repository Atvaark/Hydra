using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetDataCenterOccupationData data { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetDataCenterOccupationData data { get; set; }
    }
}
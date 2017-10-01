using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationVersionedResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetDataCenterOccupationVersionedData data { get; set; }
    }
}

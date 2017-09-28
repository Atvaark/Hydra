using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetEnvironmentsResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetEnvironmentsData data { get; set; }
    }
}
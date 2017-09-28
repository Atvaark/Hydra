using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetUsersData[] data { get; set; }
    }
}
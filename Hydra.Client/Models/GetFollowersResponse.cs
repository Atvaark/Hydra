using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetFollowersResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetFollowersData[] data { get; set; }
    }
}

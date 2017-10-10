using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserSubscribersResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetFollowersData[] data { get; set; }
    }
}

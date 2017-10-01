using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetFollowersRequest
    {
        [JsonProperty("users")]
        public UserId[] users { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetFollowersData
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("Followers")]
        public UserId[] Followers { get; set; }
    }
}
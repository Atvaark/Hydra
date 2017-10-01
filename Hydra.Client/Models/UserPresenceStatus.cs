using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UserPresenceStatus
    {
        [JsonProperty("Ratings")]
        public UserPresenceStatusRating[] Ratings { get; set; }
    }
}
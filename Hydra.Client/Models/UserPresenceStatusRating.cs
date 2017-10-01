using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UserPresenceStatusRating
    {
        [JsonProperty("RatingId")]
        public string RatingId { get; set; }

        [JsonProperty("Rating")]
        public int Rating { get; set; }
    }
}
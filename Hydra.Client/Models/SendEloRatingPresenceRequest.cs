using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendEloRatingPresenceRequest
    {
        [JsonProperty("eloRating")]
        public int eloRating { get; set; }
    }
}

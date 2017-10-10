using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ResetRatingsRequest
    {
        [JsonProperty("eloRating")]
        public int eloRating { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChallengesValue
    {
        [JsonProperty("Challenges")]
        public Challenge[] Challenges { get; set; }

        [JsonProperty("FreeRerollCount")]
        public int FreeRerollCount { get; set; }

        [JsonProperty("PurchasedRerollCount")]
        public int PurchasedRerollCount { get; set; }
    }
}
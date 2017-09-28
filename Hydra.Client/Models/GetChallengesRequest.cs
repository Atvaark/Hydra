using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetChallengesRequest
    {
    }

    public class GetChallengesResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ChallengesData data { get; set; }
    }
    
    public class ChallengesData
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Value")]
        public ChallengesValue Value { get; set; }
    }

    public class ChallengesValue
    {
        [JsonProperty("Challenges")]
        public Challenge[] Challenges { get; set; }

        [JsonProperty("FreeRerollCount")]
        public int FreeRerollCount { get; set; }

        [JsonProperty("PurchasedRerollCount")]
        public int PurchasedRerollCount { get; set; }
    }

    public class Challenge
    {
        [JsonProperty("Id")]
        public IdValue Id { get; set; }

        [JsonProperty("Definition")]
        public string Definition { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("Slot")]
        public int Slot { get; set; }

        [JsonProperty("Counters")]
        public ChallengeCounter[] Counters { get; set; }
    }

    public class IdValue
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }

    public class ChallengeCounter
    {
        [JsonProperty("Counter")]
        public IdValue Counter { get; set; }

        [JsonProperty("Definition")]
        public string Definition { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }

        [JsonProperty("Goal")]
        public int Goal { get; set; }
    }
}
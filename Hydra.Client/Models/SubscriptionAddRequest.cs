using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SubscriptionAddRequest
    {
        [JsonProperty("user")]
        public UserId user { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SubscriptionRemoveRequest
    {
        [JsonProperty("user")]
        public UserId user { get; set; }
    }
}

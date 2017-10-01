using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetSubscriptionsData
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("Subscriptions")]
        public UserId[] Subscriptions { get; set; }
    }
}

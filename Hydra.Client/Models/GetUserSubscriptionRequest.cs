using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserSubscriptionRequest
    {
        [JsonProperty("Uid")]
        public UserId[] users { get; set; }
    }
}

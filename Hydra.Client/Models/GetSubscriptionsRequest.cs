using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetSubscriptionsRequest
    {
        [JsonProperty("Uid")]
        public UserId[] users { get; set; }
    }
}

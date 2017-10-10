using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserSubscribers
    {
        [JsonProperty("users")]
        public UserId[] users { get; set; }
    }
}

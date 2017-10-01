using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SubscribeUserRequest
    {
        [JsonProperty("user")]
        public UserId user { get; set; }
    }
}

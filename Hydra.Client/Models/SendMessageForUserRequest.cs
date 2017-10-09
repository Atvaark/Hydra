using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendMessageForUserRequest
    {
        [JsonProperty("forUser")]
        public UserId forUser { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}

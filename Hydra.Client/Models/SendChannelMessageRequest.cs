using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendChannelMessageRequest
    {
        [JsonProperty("channelName")]
        public string channelName { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}

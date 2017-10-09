using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendUserIdRequest
    {
        [JsonProperty("user")]
        public UserId user { get; set; }
    }
}

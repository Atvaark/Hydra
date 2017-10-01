using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UpdatePresenceStateRequest
    {
        [JsonProperty("state")]
        public int state { get; set; }
    }
}

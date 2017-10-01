using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UserPresence
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Status")]
        public UserPresenceStatus Status { get; set; }
    }
}
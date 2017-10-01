using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPresenceMatchmake5Request
    {
        [JsonProperty("userSessionEventSequenceId")]
        public int userSessionEventSequenceId { get; set; }
    }
}

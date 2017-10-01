using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPresenceSquad5Request
    {
        [JsonProperty("inviteEventSequenceId")]
        public int inviteEventSequenceId { get; set; }
        
        [JsonProperty("squadEventSequenceId")]
        public int squadEventSequenceId { get; set; }
    }
}

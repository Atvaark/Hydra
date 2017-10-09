using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadInviteRequest
    {
        [JsonProperty("userId")]
        public UserId userId { get; set; }
    }
}
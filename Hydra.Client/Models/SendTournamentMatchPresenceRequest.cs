using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendTournamentMatchPresenceRequest
    {
        [JsonProperty("tournamentMatchId")]
        public string tournamentMatchId { get; set; }
    }
}

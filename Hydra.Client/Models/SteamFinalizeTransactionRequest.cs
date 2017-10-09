using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SteamFinalizeTransactionRequest : ServiceResult
    {
        [JsonProperty("steamId")]
        public string steamId { get; set; }
        
        [JsonProperty("offerId")]
        public int offerId { get; set; }
    }
}

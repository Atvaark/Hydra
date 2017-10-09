using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SteamInitTransactionRequest
    {
        [JsonProperty("steamId")]
        public string steamId { get; set; }

        [JsonProperty("languageCode")]
        public string languageCode { get; set; }

        [JsonProperty("offerId")]
        public int offerId { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }
    }
}

using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class OfferTransactionRequest
    {
        [JsonProperty("offerIdContracts")]
        public OfferIdContract[] offerIdContracts { get; set; }

        [JsonProperty("fromTransactionId")]
        public int fromTransactionId { get; set; }

        [JsonProperty("count")]
        public object count { get; set; }
    }
}

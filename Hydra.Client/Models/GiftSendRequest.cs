using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GiftSendRequest
    {
        [JsonProperty("gift")]
        public OfferIdContract gift { get; set; }

        [JsonProperty("recipient")]
        public UserId recipient { get; set; }
    }
}

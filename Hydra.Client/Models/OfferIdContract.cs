using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class OfferIdContract
    {
        [JsonProperty("OfferId")]
        public string OfferId { get; set; }

        [JsonProperty("ReferenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("Price")]
        public object Price { get; set; }
    }
}
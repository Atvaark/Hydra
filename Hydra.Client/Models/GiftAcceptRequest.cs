using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GiftAcceptRequest
    {
        [JsonProperty("giftId")]
        public string giftId { get; set; }
    }
}

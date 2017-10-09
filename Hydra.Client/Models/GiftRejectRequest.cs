using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GiftRejectRequest
    {
        [JsonProperty("giftId")]
        public string giftId { get; set; }
    }
}

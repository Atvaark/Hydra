using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UserInventoryData
    {
        [JsonProperty("LastTransactionId")]
        public int LastTransactionId { get; set; }

        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("UserStateList")]
        public UserState[] UserStateList { get; set; }
    }
}
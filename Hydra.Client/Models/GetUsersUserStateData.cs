using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersUserStateData
    {
        [JsonProperty("OwnType")]
        public int OwnType { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }

        [JsonProperty("StateName")]
        public string StateName { get; set; }
    }
}
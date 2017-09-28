using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersData
    {
        [JsonProperty("User")]
        public User User { get; set; }

        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [JsonProperty("UserStateData")]
        public GetUsersUserStateData[] UserStateData { get; set; }
    }
}
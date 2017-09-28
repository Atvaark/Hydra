using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginToken
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }
    }
}
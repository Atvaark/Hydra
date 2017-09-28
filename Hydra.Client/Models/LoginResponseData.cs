using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginResponseData
    {
        [JsonProperty("Token")]
        public LoginToken Token { get; set; }

        [JsonProperty("Position")]
        public int Position { get; set; }
    }
}
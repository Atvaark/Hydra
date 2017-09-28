using Newtonsoft.Json;

namespace Hydra.Client.Models.Auth
{
    public class AuthRequest
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("session_id")]
        public string session_id { get; set; }
    }
}
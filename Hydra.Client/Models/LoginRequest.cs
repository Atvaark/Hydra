using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginRequest
    {
        [JsonProperty("login")]
        public string login { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("provider")]
        public string provider { get; set; }

        [JsonProperty("signature")]
        public LoginSignature signature { get; set; }

        [JsonProperty("structureVersion")]
        public string structureVersion { get; set; }

        [JsonProperty("clientData")]
        public LoginClientData clientData { get; set; }
    }
}
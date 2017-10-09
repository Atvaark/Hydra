using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownLoginRequest
    {
        [JsonProperty("token")]
        public string token { get; set; }

        [JsonProperty("signature")]
        public LoginSignature signature { get; set; }

        [JsonProperty("structureVersion")]
        public string structureVersion { get; set; }

        [JsonProperty("clientData")]
        public LoginClientData clientData { get; set; }
    }
}

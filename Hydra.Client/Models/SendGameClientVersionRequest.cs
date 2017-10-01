using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendGameClientVersionRequest
    {
        [JsonProperty("gameClientVersion")]
        public string gameClientVersion { get; set; }

        [JsonProperty("signature")]
        public LoginSignature signature { get; set; }
    }
}

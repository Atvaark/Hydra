using Newtonsoft.Json;

namespace Hydra.Client.Models.Auth
{
    public class VerifyRequest
    {
        [JsonProperty("session_id")]
        public string session_id { get; set; }
    }
}
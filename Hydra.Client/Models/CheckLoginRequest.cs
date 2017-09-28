using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class CheckLoginRequest
    {
        [JsonProperty("token")]
        public string token { get; set; }
    }
}
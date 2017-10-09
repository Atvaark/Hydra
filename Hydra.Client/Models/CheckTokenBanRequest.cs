using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class CheckTokenBanRequest
    {
        [JsonProperty("token")]
        public string token { get; set; }
    }
}

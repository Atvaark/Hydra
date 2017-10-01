using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UpdateClientContextRequest
    {
        [JsonProperty("clientContext")]
        public string clientContext { get; set; }
    }
}

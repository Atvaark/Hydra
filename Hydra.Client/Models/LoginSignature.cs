using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginSignature
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
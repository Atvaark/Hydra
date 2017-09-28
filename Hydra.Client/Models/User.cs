using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class User
    {
        [JsonProperty("Uid")]
        public string Uid { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UserId
    {
        [JsonProperty("Uid")]
        public string Uid { get; set; }
    }
}
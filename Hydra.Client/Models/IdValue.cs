using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class IdValue
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
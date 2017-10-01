using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GameSessionId
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
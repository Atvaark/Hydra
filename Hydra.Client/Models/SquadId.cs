using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadId
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class PartyId
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
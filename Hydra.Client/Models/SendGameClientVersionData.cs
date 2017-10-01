using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendGameClientVersionData
    {
        [JsonProperty("ReturnAvailableSeconds")]
        public int ReturnAvailableSeconds { get; set; }
    }
}
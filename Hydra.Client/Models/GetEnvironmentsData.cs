using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetEnvironmentsData
    {
        [JsonProperty("Environments")]
        public EnvironmentData[] Environments { get; set; }

        [JsonProperty("Date")]
        public long Date { get; set; }
    }
}
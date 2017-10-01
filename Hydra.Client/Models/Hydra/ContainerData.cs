using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ContainerData
    {
        [JsonProperty("ContainerName")]
        public string ContainerName { get; set; }

        [JsonProperty("Records")]
        public ContainerKeyRecord[] Records { get; set; }
    }
}
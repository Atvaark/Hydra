using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ContainerKeyRecord
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Record")]
        public ContainerRecord Record { get; set; }
    }
}
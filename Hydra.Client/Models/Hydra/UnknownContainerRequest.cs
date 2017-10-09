using Hydra.Client.Models.Hydra;
using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownContainerRequest
    {
        [JsonProperty("containerName")]
        public string containerName { get; set; }

        [JsonProperty("dataList")]
        public ContainerKeyRecord[] dataList { get; set; }
    }
}

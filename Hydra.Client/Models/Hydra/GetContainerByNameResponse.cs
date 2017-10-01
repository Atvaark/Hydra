using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByNameResponse : HydraServiceResult
    {
        [JsonProperty("data")]
        public ContainerData data { get; set; }
    }
}

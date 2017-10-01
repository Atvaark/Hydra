using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByNameResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ContainerData data { get; set; }
    }
}

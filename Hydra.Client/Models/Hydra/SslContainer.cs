using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class SslContainer<T> : HydraServiceData where T : SslType
    {
        [JsonProperty("SslType")]
        public string SslType { get; set; }

        [JsonProperty("SslValue")]
        public T SslValue { get; set; }
    }
}
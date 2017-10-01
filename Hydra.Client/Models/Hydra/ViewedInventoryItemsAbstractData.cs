using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ViewedInventoryItemsAbstractData : SslType
    {
        [JsonProperty("data")]
        public ViewedInventoryItem[] data { get; set; }
    }
}
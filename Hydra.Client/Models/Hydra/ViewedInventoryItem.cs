using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ViewedInventoryItem
    {
        [JsonProperty("itemId")]
        public string itemId { get; set; }

        [JsonProperty("count")]
        public int count { get; set; }
    }
}
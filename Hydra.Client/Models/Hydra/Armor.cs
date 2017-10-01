using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class Armor
    {
        [JsonProperty("armorUid")]
        public int armorUid { get; set; }

        [JsonProperty("vanityAttachIndexess")]
        public object[] vanityAttachIndexess { get; set; }
    }
}
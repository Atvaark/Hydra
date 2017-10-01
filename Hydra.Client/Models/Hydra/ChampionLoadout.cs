using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ChampionLoadout
    {
        [JsonProperty("characterIndex")]
        public int characterIndex { get; set; }

        [JsonProperty("helmetIds")]
        public Armor helmetIds { get; set; }

        [JsonProperty("upperBodyIds")]
        public Armor upperBodyIds { get; set; }

        [JsonProperty("lowerBodyIds")]
        public Armor lowerBodyIds { get; set; }

        [JsonProperty("shaderId")]
        public int shaderId { get; set; }

        [JsonProperty("vanityIndex")]
        public int vanityIndex { get; set; }

        [JsonProperty("level")]
        public int level { get; set; }

        [JsonProperty("startingWeapon")]
        public int startingWeapon { get; set; }

        [JsonProperty("skins")]
        public object[] skins { get; set; }

        [JsonProperty("weaponShaders")]
        public object[] weaponShaders { get; set; }
    }
}
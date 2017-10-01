using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class ChampionLoadoutAbstractDataList : SslType
    {
        [JsonProperty("selectedCharacterIndex")]
        public int selectedCharacterIndex { get; set; }

        [JsonProperty("championLoadouts")]
        public ChampionLoadout[] championLoadouts { get; set; }

        [JsonProperty("viewedGameSessionId")]
        public string viewedGameSessionId { get; set; }

        [JsonProperty("avatartItemName")]
        public string avatartItemName { get; set; }

        [JsonProperty("namePlateItemName")]
        public string namePlateItemName { get; set; }

        [JsonProperty("viewedMessages")]
        public int[] viewedMessages { get; set; }

        [JsonProperty("viewedMessagesVersion")]
        public int viewedMessagesVersion { get; set; }
    }
}
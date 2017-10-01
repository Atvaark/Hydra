using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByNameResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ContainerData data { get; set; }
    }

    public class ContainerData
    {
        [JsonProperty("ContainerName")]
        public string ContainerName { get; set; }

        [JsonProperty("Records")]
        public ContainerKeyRecord[] Records { get; set; }
    }

    public class ContainerKeyRecord
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Record")]
        public ContainerRecord Record { get; set; }
    }

    public class ContainerRecord
    {
        [JsonProperty("Layout")]
        public int Layout { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Data")]
        public int[] Data { get; set; }
    }
    

    public class SslRootObject
    {
        [JsonProperty("SslType")]
        public string SslType { get; set; }

        [JsonProperty("SslValue")]
        public SslType SslValue { get; set; }
    }

    public abstract class SslType
    {
    }

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

    public class Armor
    {
        [JsonProperty("armorUid")]
        public int armorUid { get; set; }

        [JsonProperty("vanityAttachIndexess")]
        public object[] vanityAttachIndexess { get; set; }
    }
}

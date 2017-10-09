using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UpgradeRuneRequest
    {
        [JsonProperty("transactionType")]
        public int transactionType { get; set; }

        [JsonProperty("itemDesc")]
        public string itemDesc { get; set; }

        [JsonProperty("runeToUpgrade")]
        public string runeToUpgrade { get; set; }

        [JsonProperty("upgradeComponents")]
        public string[] upgradeComponents { get; set; }
    }
}

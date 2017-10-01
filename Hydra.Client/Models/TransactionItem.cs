using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TransactionItem
    {
        [JsonProperty("StateName")]
        public string StateName { get; set; }

        [JsonProperty("OwnType")]
        public int OwnType { get; set; }

        [JsonProperty("OperationType")]
        public int OperationType { get; set; }

        [JsonProperty("InitialValue")]
        public int InitialValue { get; set; }

        [JsonProperty("ResultingValue")]
        public int ResultingValue { get; set; }

        [JsonProperty("DeltaValue")]
        public int DeltaValue { get; set; }

        [JsonProperty("DescId")]
        public int DescId { get; set; }

        [JsonProperty("ExtendedInfoItems")]
        public TransactionExtendedInfoItem ExtendedInfoItems { get; set; }
    }
}
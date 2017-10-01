using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class Transaction
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("TransactionItems")]
        public TransactionItem[] TransactionItems { get; set; }

        [JsonProperty("ReferenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("OfferId")]
        public string OfferId { get; set; }

        [JsonProperty("OperationType")]
        public int OperationType { get; set; }

        [JsonProperty("SourceType")]
        public int SourceType { get; set; }

        [JsonProperty("ExtendedInfoItems")]
        public TransactionExtendedInfoItem[] ExtendedInfoItems { get; set; }
    }
}
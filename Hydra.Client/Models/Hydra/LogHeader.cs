using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class LogHeader
    {
        [JsonProperty("SessionId")]
        public string SessionId { get; set; }

        [JsonProperty("Format")]
        public string Format { get; set; }

        [JsonProperty("MajorVersion")]
        public int MajorVersion { get; set; }

        [JsonProperty("MinorVersion")]
        public int MinorVersion { get; set; }

        [JsonProperty("PackNumber")]
        public int PackNumber { get; set; }

        [JsonProperty("Flags")]
        public int Flags { get; set; }

        [JsonProperty("InitTime")]
        public long InitTime { get; set; }

        [JsonProperty("Context")]
        public LogHeaderContext[] Context { get; set; }

        [JsonProperty("StartTime")]
        public long StartTime { get; set; }

        [JsonProperty("EndTime")]
        public long EndTime { get; set; }

        [JsonProperty("FinalizationTimeout")]
        public int FinalizationTimeout { get; set; }
    }
}
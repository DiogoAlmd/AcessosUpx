using System.Text.Json.Serialization;

namespace ApiAcesso.Models
{
    public class GetLocationModel
    {
        [JsonPropertyName("ip")]
        public string ip { get; set; }

        [JsonPropertyName("hostname")]
        public string hostname { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("region")]
        public string region { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("loc")]
        public string loc { get; set; }

        [JsonPropertyName("org")]
        public string org { get; set; }

        [JsonPropertyName("postal")]
        public string postal { get; set; }

        [JsonPropertyName("timezone")]
        public string timezone { get; set; }

        [JsonPropertyName("readme")]
        public string readme { get; set; }
    }
}

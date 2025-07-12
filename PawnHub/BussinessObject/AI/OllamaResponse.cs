using System.Text.Json.Serialization;

namespace BussinessObject.AI
{
    public class OllamaResponse
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("context")]
        public List<string> Context { get; set; }

        [JsonIgnore]
        public string Response => Message?.Content;
    }

}

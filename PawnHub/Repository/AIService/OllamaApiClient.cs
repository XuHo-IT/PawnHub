using BussinessObject.AI;
using System.Net.Http.Json;

namespace Repository.AIService
{
    public class OllamaApiClient : IDisposable
    {
        private readonly HttpClient _client;

        public OllamaApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:11434")
            };
        }

        public OllamaCompletion Completions => new OllamaCompletion(_client);

        public void Dispose() => _client?.Dispose();
    }

    public class OllamaCompletion
    {
        private readonly HttpClient _client;

        public OllamaCompletion(HttpClient client)
        {
            _client = client;
        }

        public async Task<OllamaResponse> GenerateChatAsync(ChatModel model)
        {
            var payload = new
            {
                model = model.Model,
                messages = model.Messages.Select(m => new { role = m.Role, content = m.Content }),
                stream = false
            };

            var response = await _client.PostAsJsonAsync("/api/chat", payload);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OllamaResponse>();
        }





    }
}

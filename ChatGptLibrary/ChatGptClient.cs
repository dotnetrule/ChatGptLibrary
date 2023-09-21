using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatGptLibrary
{
    public class ChatGptClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public ChatGptClient(string apiKey)
        {
            _apiKey = apiKey;
            _apiUrl = "https://api.openai.com/v1/engines/davinci/completions";

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateResponse(string prompt, int maxTokens = 50)
        {
            var requestData = new 
            {
                prompt = prompt,
                max_tokens = maxTokens
            };
            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }

}
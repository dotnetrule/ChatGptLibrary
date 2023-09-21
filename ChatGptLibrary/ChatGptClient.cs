using ChatGptLibrary.Helpers;
using ChatGptLibrary.Responses;
using System;
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
        private string _model;


        public ChatGptClient(string apiKey, string organizationId)
        {
            _apiKey = apiKey;
            _apiUrl = "https://api.openai.com/v1/chat/completions";
            _model = EnumHelper.GetEnumDescription(GPTModels.GPT4);

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _client.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organizationId}");
        }

        public async Task<ChatCompletionResponse> GenerateResponse(string prompt, double temperature = 0.7)
        {
            var jsonResponse = await GenerateResponseAsString(prompt, temperature);
            ChatCompletionResponse response = JsonSerializer.Deserialize<ChatCompletionResponse>(jsonResponse);
            return response;
        }

        public async Task<string> GenerateResponseAsString(string prompt, double temperature = 0.7)
        {
            var requestData = new 
            {
                model = _model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = temperature
            };

            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                Console.Write($"Response; {responseData}");

                return responseData;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException}");
                throw;

            }
        }

        public void SetModel(GPTModels model)
        {
            this._model = EnumHelper.GetEnumDescription(model);
        }
    }

}
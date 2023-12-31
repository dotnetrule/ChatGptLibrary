﻿using ChatGptLibrary.Helpers;
using ChatGptLibrary.Responses;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
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
        private string _model { get; set; }


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

        public async Task<string> GenerateResponseAsString(string prompt, double temperature = 0)
        {
            var requestData = new 
            {
                model = _model,
                max_tokens= 256,
                messages = new object[]
                {
                    
                    new { 
                        role = "user", 
                        content = prompt
                    },
                    new {
                        role = "system",
                        content = "Handle this as short and consise as possible."
                    }
                },
                temperature = temperature
            };

            return await GenerateResponseAsString(requestData);
        }


        public async Task<string> GenerateResponseAsString(object requestData)
        {
            
            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response; {responseData}");

                return responseData;
            }
            catch (Exception ex)
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
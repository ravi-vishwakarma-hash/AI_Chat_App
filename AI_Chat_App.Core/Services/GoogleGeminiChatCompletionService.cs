using AI_Chat_App.Core.Interfaces;
using AI_Chat_App.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AI_Chat_App.Core.Services
{
    public sealed class GoogleGeminiChatCompletionService( HttpClient httpClient, string ModelId, string ApiKey) 
        : IChatCompletionService
    {
        public HttpClient HttpClient { get; } = httpClient;
        public string ModelId { get; } = ModelId;
        public string ApiKey { get; } = ApiKey;

        public async Task<string> GetResponseAsync(string prompt)
        {
            // Implement the logic to call the Google Gemini API using HttpClient and return a GeneralChatMessageResponse
            var request = new
            {
                model = ModelId,
                input = prompt
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/interactions");

            httpRequest.Headers.Add("x-goog-api-key", ApiKey);

            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), 
                Encoding.UTF8, 
                "application/json"
            );

            var response = await HttpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                string responceContent = await response.Content.ReadAsStringAsync();
                return responceContent;
                 
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, reason: {response.ReasonPhrase}");
            }
        }
    }
}

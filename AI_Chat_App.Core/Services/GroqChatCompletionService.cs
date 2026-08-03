using AI_Chat_App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Chat_App.Core.Services
{
    public sealed class GroqChatCompletionService(string ModelId, string ApiKey) 
        : IChatCompletionService
    {
        public string ModelId { get; } = ModelId;
        public string ApiKey { get; } = ApiKey;

        public Task<string> GetResponseAsync(string prompt)
        {
            throw new NotImplementedException();
        }
    }
}

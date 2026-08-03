using AI_Chat_App.Core.Models;

namespace AI_Chat_App.Core.Interfaces
{
    public interface IChatCompletionService
    {
        Task<string> GetResponseAsync(string prompt);
    }
}

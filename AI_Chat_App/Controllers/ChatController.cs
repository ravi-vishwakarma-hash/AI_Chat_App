using AI_Chat_App.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AI_Chat_App.Controllers
{
    public class ChatController(IServiceProvider serviceProvider) : Controller
    {
        public IServiceProvider ServiceProvider { get; } = serviceProvider;

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Chat( string ModelId, string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }
            try
            {
                var _service = ServiceProvider.GetRequiredKeyedService<IChatCompletionService>(ModelId);

                var response = await _service.GetResponseAsync(prompt);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

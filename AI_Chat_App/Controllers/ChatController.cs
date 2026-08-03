using AI_Chat_App.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AI_Chat_App.Controllers
{
    public class ChatController([FromKeyedServices("Google")] IChatCompletionService service) : Controller
    {
        public IChatCompletionService Service { get; } = service;

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Chat(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }
            try
            {
                var response = await Service.GetResponseAsync(prompt);
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

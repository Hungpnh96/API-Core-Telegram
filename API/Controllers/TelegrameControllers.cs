using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using static API.Models.SendMessToTelegram;

namespace API.Controllers
{
    [Route("api/v1/telegram")]
    [ApiController]
    public class TelegrameControllers : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TelegrameControllers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessRequest request)
        {
            var token = _configuration["Telegram:BotToken"];
            var chatId = _configuration["Telegram:ChatId"];

            var message = new
            {
                chat_id = chatId,
                text = $"Tên người gửi: {request.SenderName}\nGửi từ: {request.From}\nNội dung: {request.Content}"
            };

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(
                $"https://api.telegram.org/bot{token}/sendMessage", message
            );

            return response.IsSuccessStatusCode
                ? Ok("Tin nhắn đã được gửi thành công!")
                : BadRequest("Không thể gửi tin nhắn.");
        }

    }
}

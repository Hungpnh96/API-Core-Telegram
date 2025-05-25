using API.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static API.Models.SendMessToTelegram;

namespace API.Controllers
{
    [Route("api/v1/discord")]
    [ApiController]
    public class DiscordConllers : ControllerBase
    {
        private IDiscordService _discordService;
        public DiscordConllers(IDiscordService discordService)
        {
            _discordService = discordService;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello Discord!");
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessRequest request)
        {

            var response = await _discordService.SendMessageAsync(request);

            if (response.Code.Equals(200))
            {
                return Ok(new SendMessResponse
                {
                    Code = 200,
                    Message = "Tin nhắn đã được gửi thành công!"
                });
            }
            else
            {
                return BadRequest(response.Message);
            }

        }

    }
}

using API.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static API.Models.SendMessToTelegram;

namespace API.Service
{
    public class DiscordService : IDiscordService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _webhookURL = "";
        private readonly string _webhookKey = "";
        private readonly string _webhookToken = "";
        public DiscordService(IConfiguration configuration) 
        {
            _configuration = configuration;

            _webhookURL = _configuration["Discord:WebhooksURL"];
            _webhookKey = _configuration["Discord:WebhooksKeyWedding"];
            _webhookToken = _configuration["Discord:WebhooksTokenWedding"];
        }

        public async Task<SendMessResponse> SendMessageAsync(SendMessRequest request)
        {
            SendMessResponse sendMessResponse = new SendMessResponse();
            try
            {
                var message = new
                {
                    content = $"===================================\n\nTên người gửi: {request.SenderName}\nBạn của: {request.From}\nNội dung: {request.Content}\n\n==================================="
                };

                var json = JsonSerializer.Serialize(message);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_webhookURL}/{_webhookKey}/{_webhookToken}", httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    sendMessResponse = new SendMessResponse
                    {
                        Code = 400 ,
                        Message = $"Discord Webhook Error {response.StatusCode}: {errorBody}"
                    };
                }
                else
                {
                    sendMessResponse = new SendMessResponse
                    {
                        Code = 200,
                        Message = "Tin nhắn đã được gửi thành công!"
                    };
                }
            }
            catch(Exception ex)
            {
                sendMessResponse = new SendMessResponse
                {
                    Code = 400,
                    Message = $"Discord Webhook Catch {ex.Message}"
                };
            }
           return sendMessResponse;
        }
    }
}

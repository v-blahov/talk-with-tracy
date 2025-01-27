using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {

        public WeatherForecastController()
        {
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Post([FromBody] MessagePayload payload)
        {
            var httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://api.tereza.ai/chat");
            string jsonString = System.Text.Json.JsonSerializer.Serialize(payload);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var res = await httpclient.PostAsync("", content);
            string responseBody = await res.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }

        public class MessagePayload
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("thread_id")]
            public string? Thread_id { get; set; }
        }
    }
}

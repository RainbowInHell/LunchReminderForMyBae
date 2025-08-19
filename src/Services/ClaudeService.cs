using System.Text;
using System.Text.Json;
using LunchReminderBot.Models;

namespace LunchReminderBot.Services;

public sealed class ClaudeService : IDisposable
{
    private readonly HttpClient _httpClient;

    public ClaudeService(string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "LunchReminderBot/1.0");
    }

    public async Task<ReminderMessage> GenerateReminderAsync()
    {
        try
        {
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("Belarus Standard Time")
            );

            var dayOfWeek = currentTime.DayOfWeek switch
            {
                DayOfWeek.Monday => "Monday",
                DayOfWeek.Tuesday => "Tuesday", 
                DayOfWeek.Wednesday => "Wednesday",
                DayOfWeek.Thursday => "Thursday",
                DayOfWeek.Friday => "Friday",
                _ => "today"
            };

            var prompt = $@"Generate a sweet, loving reminder message for my girlfriend to order lunch. 
It's {dayOfWeek} and the current time is {currentTime:HH:mm}.

Requirements:
- Be warm, caring, and romantic
- Keep it brief (1-3 sentences)
- Remind her to order lunch
- Make it feel personal and loving
- Use different phrasing each time
- Add some cute emojis
- Sometimes mention it's {dayOfWeek}

Examples of tone:
- ""Good morning my love! 💕 Don't forget to order your lunch today, I want to make sure you eat well! 🥗✨""
- ""Hey beautiful! 😘 Time for lunch planning - treat yourself to something delicious! 🍽️💖""

Generate a unique message now:";

            var requestBody = new
            {
                model = "claude-3-sonnet-20240229",
                max_tokens = 100,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.anthropic.com/v1/messages", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonSerializer.Deserialize<JsonElement>(responseJson);
                
                var messageText = responseObj.GetProperty("content")[0].GetProperty("text").GetString() ?? 
                                 "Good morning my love! 💕 Don't forget to order your lunch today! 🥗✨";

                return new ReminderMessage(messageText.Trim(), currentTime);
            }
            else
            {
                Console.WriteLine($"Claude API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                throw new Exception($"Claude API returned {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate message with Claude: {ex.Message}");
            
            // Fallback message
            var fallbackMessage = "Good morning beautiful! 💕 Don't forget to order your lunch today - I want to make sure you eat well! 🥗✨";
            return new ReminderMessage(fallbackMessage, DateTime.Now);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
using LunchReminderBot.Models;

namespace LunchReminderBot.Services;

public class MessageGenerator
{
    private readonly ClaudeService _claudeService;
    private readonly TelegramService _telegramService;

    public MessageGenerator(ClaudeService claudeService, TelegramService telegramService)
    {
        _claudeService = claudeService;
        _telegramService = telegramService;
    }

    public async Task<bool> SendLunchReminderAsync()
    {
        try
        {
            Console.WriteLine("Generating personalized lunch reminder...");
            
            var message = await _claudeService.GenerateReminderAsync();
            
            Console.WriteLine($"Generated message: {message.Text}");
            
            var success = await _telegramService.SendReminderAsync(message);
            
            if (success)
            {
                Console.WriteLine("Lunch reminder sent successfully! 💕");
            }
            else
            {
                Console.WriteLine("Failed to send lunch reminder.");
            }
            
            return success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending lunch reminder: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> TestServicesAsync()
    {
        Console.WriteLine("Testing Telegram connection...");
        var telegramOk = await _telegramService.TestConnectionAsync();
        
        if (telegramOk)
        {
            Console.WriteLine("Testing Claude message generation...");
            try
            {
                var testMessage = await _claudeService.GenerateReminderAsync();
                Console.WriteLine($"Test message generated: {testMessage.Text}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Claude test failed: {ex.Message}");
                return false;
            }
        }
        
        return false;
    }
}
namespace LunchReminderBot.Services;

public class MessageGenerator(string telegramBotToken, string telegramChatId)
{
    private readonly TelegramService _telegramService = new(telegramBotToken, telegramChatId);

    public async Task<bool> SendLunchReminderAsync()
    {
        try
        {
            Console.WriteLine("Generating personalized lunch reminder...");
            
            var message = MessageService.GenerateReminder();
            
            Console.WriteLine($"Generated message: {message}");
            var success = await _telegramService.SendReminderAsync(message);

            Console.WriteLine(success ? "Lunch reminder sent successfully! 💕" : "Failed to send lunch reminder.");

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
        return await _telegramService.TestConnectionAsync();
    }
}
using Telegram.Bot;

namespace LunchReminderBot.Services;

public class TelegramService(string botToken, string chatId)
{
    private readonly TelegramBotClient _client = new(botToken);
    private readonly long _chatId = long.Parse(chatId);

    public async Task<bool> SendReminderAsync(string message)
    {
        try
        {
            _ = await _client.SendMessage(chatId: _chatId, text: message, cancellationToken: CancellationToken.None);

            Console.WriteLine($"Message sent successfully at {DateTime.Now:HH:mm:ss}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send message: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var me = await _client.GetMe();
            Console.WriteLine($"Bot connected successfully: @{me.Username}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to Telegram: {ex.Message}");
            return false;
        }
    }
}
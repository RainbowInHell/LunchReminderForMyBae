using Telegram.Bot;
using Telegram.Bot.Types;
using LunchReminderBot.Models;

namespace LunchReminderBot.Services;

public class TelegramService
{
    private readonly TelegramBotClient _client;
    private readonly long _chatId;

    public TelegramService(string botToken, string chatId)
    {
        _client = new TelegramBotClient(botToken);
        _chatId = long.Parse(chatId);
    }

    public async Task<bool> SendReminderAsync(ReminderMessage message)
    {
        try
        {
            var sentMessage = await _client.SendTextMessageAsync(
                chatId: _chatId,
                text: message.Text,
                cancellationToken: CancellationToken.None
            );

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
            var me = await _client.GetMeAsync();
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
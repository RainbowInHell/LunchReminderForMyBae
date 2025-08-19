using LunchReminderBot.Services;
using Microsoft.Extensions.Configuration;

Console.WriteLine("🍽️ Lunch Reminder Bot Starting...");

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var telegramBotToken = configuration["TELEGRAM_BOT_TOKEN"];
var telegramChatId = configuration["TELEGRAM_CHAT_ID"];

if (string.IsNullOrEmpty(telegramBotToken))
{
    Console.WriteLine("❌ TELEGRAM_BOT_TOKEN environment variable is required");
    return 1;
}

if (string.IsNullOrEmpty(telegramChatId))
{
    Console.WriteLine("❌ TELEGRAM_CHAT_ID environment variable is required");
    return 1;
}

try
{
    var messageGenerator = new MessageGenerator(telegramBotToken, telegramChatId);

    if (args.Contains("--test"))
    {
        Console.WriteLine("🔧 Running in test mode...");

        var testSuccess = await messageGenerator.TestServicesAsync();
        if (!testSuccess)
        {
            Console.WriteLine("❌ Service tests failed");
            return 1;
        }

        Console.WriteLine("✅ All services tested successfully");
        return 0;
    }

    var success = await messageGenerator.SendLunchReminderAsync();
    if (success)
    {
        Console.WriteLine("✅ Lunch reminder bot completed successfully!");
        return 0;
    }

    Console.WriteLine("❌ Failed to send lunch reminder");
    return 1;
}
catch (Exception ex)
{
    Console.WriteLine($"💥 Unexpected error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    return 1;
}

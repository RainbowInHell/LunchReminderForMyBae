using LunchReminderBot.Services;
using Microsoft.Extensions.Configuration;

namespace LunchReminderBot;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("🍽️ Lunch Reminder Bot Starting...");
        
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        // Get configuration from environment variables
        var telegramBotToken = configuration["TELEGRAM_BOT_TOKEN"];
        var telegramChatId = configuration["TELEGRAM_CHAT_ID"];  
        var claudeApiKey = configuration["CLAUDE_API_KEY"];

        // Validate configuration
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

        if (string.IsNullOrEmpty(claudeApiKey))
        {
            Console.WriteLine("❌ CLAUDE_API_KEY environment variable is required");
            return 1;
        }

        // Check if it's a weekday and within the time window
        var minsk = TimeZoneInfo.FindSystemTimeZoneById("Belarus Standard Time");
        var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, minsk);
        
        Console.WriteLine($"Current time in Minsk: {currentTime:yyyy-MM-dd HH:mm:ss}");

        // Check if it's a weekday (Monday-Friday)
        if (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday)
        {
            Console.WriteLine("📅 It's weekend - no lunch reminder needed");
            return 0;
        }

        // Check if it's within the time window (8:00-8:30 AM)
        var reminderTime = new TimeOnly(8, 0); // 8:00 AM
        var endTime = new TimeOnly(8, 30); // 8:30 AM
        var currentTimeOnly = TimeOnly.FromDateTime(currentTime);

        if (currentTimeOnly < reminderTime || currentTimeOnly > endTime)
        {
            Console.WriteLine($"⏰ Current time {currentTimeOnly:HH:mm} is outside reminder window (08:00-08:30)");
            
            // Add a small random delay if we're in development/testing mode
            if (args.Contains("--test"))
            {
                Console.WriteLine("🧪 Test mode - sending reminder anyway");
            }
            else
            {
                return 0;
            }
        }

        try
        {
            // Initialize services
            var telegramService = new TelegramService(telegramBotToken, telegramChatId);
            using var claudeService = new ClaudeService(claudeApiKey);
            var messageGenerator = new MessageGenerator(claudeService, telegramService);

            // Test mode - verify connections
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
                
                if (args.Contains("--send-test"))
                {
                    Console.WriteLine("📤 Sending test message...");
                    var sendSuccess = await messageGenerator.SendLunchReminderAsync();
                    return sendSuccess ? 0 : 1;
                }
                
                return 0;
            }

            // Add random delay between 0-30 minutes to vary timing
            var random = new Random();
            var delayMinutes = random.Next(0, 31); // 0-30 minutes
            
            if (delayMinutes > 0)
            {
                Console.WriteLine($"⏳ Adding random delay of {delayMinutes} minutes...");
                await Task.Delay(TimeSpan.FromMinutes(delayMinutes));
            }

            // Send the lunch reminder
            var success = await messageGenerator.SendLunchReminderAsync();
            
            if (success)
            {
                Console.WriteLine("✅ Lunch reminder bot completed successfully!");
                return 0;
            }
            else
            {
                Console.WriteLine("❌ Failed to send lunch reminder");
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Unexpected error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return 1;
        }
    }
}
# 🍽️ Lunch Reminder Bot for My Bae

A sweet and simple Telegram bot that sends personalized lunch reminders to your girlfriend every weekday between 8:00-8:30 AM (Minsk time). The bot uses pre-written romantic messages that vary by day of the week.

## ✨ Features

- 💕 Sends personalized, romantic lunch reminders
- 📝 Pre-written messages that vary by day of the week for variety
- ⏰ Runs automatically Monday-Friday at 8:00-8:30 AM Minsk time
- 🔒 Secure API key management with GitHub Secrets
- 🆓 Completely free using GitHub Actions (no external APIs required)
- 🎲 Random timing within the 30-minute window for natural feel

## 🚀 Setup Instructions

### Step 1: Create a Telegram Bot

1. Message [@BotFather](https://t.me/botfather) on Telegram
2. Send `/newbot` command
3. Follow the prompts to name your bot
4. Save the **Bot Token** (you'll need this later)

### Step 2: Get Your Chat ID

1. Start a conversation with your bot (send any message)
2. Visit: `https://api.telegram.org/bot<YOUR_BOT_TOKEN>/getUpdates`
3. Look for `"chat":{"id":` in the response
4. Save the **Chat ID** number (you'll need this later)

### Step 3: Fork This Repository

1. Fork this repository to your GitHub account
2. Clone your forked repository locally

### Step 4: Configure GitHub Secrets

1. Go to your forked repository on GitHub
2. Navigate to Settings → Secrets and variables → Actions
3. Create these Repository secrets:
   - `TELEGRAM_BOT_TOKEN`: Your bot token from Step 1
   - `TELEGRAM_CHAT_ID`: Your chat ID from Step 2

### Step 5: Test the Bot

1. Go to Actions tab in your GitHub repository
2. Click on "🍽️ Lunch Reminder Bot" workflow
3. Click "Run workflow" to test manually
4. Check the logs to ensure everything works

## 🏗️ Project Structure

```
LunchReminderForMyBae/
├── src/
│   ├── Program.cs                 # Main application entry point
│   ├── Services/
│   │   ├── TelegramService.cs     # Telegram API integration
│   │   ├── MessageService.cs      # Pre-written message generation
│   │   └── MessageGenerator.cs    # Message generation logic
│   └── Models/
│       └── ReminderMessage.cs     # Data models
├── LunchReminderBot.csproj        # .NET project file
├── .github/
│   └── workflows/
│       └── lunch-reminder.yml     # GitHub Actions workflow
└── README.md                      # This file
```

## 🧪 Local Testing

To test the bot locally:

1. Set environment variables:
```bash
export TELEGRAM_BOT_TOKEN="your_bot_token"
export TELEGRAM_CHAT_ID="your_chat_id"
```

2. Run tests:
```bash
dotnet run -- --test
```

3. Send a test message:
```bash
dotnet run -- --test --send-test
```

## ⚙️ How It Works

1. **Scheduled Execution**: GitHub Actions runs the workflow every weekday at 8:00 AM Minsk time
2. **Time Validation**: The bot checks if it's a weekday and within the 8:00-8:30 AM window
3. **Random Delay**: Adds 0-30 minutes random delay for natural timing
4. **Message Generation**: Selects a random romantic message based on the day of the week
5. **Message Delivery**: Sends the personalized message via Telegram

## 💝 Sample Messages

The bot sends different messages for each day:

**Monday Messages:**
- "Good morning my love! 💕 It's Monday - let's start the week with a delicious lunch! 🥗✨"
- "Hey beautiful! 😘 Monday lunch time - treat yourself to something amazing! 🍽️💖"

**Tuesday Messages:**
- "Good morning beautiful! 💕 Tuesday vibes - time for a delightful lunch! 🥗✨"
- "Hey my love! 😘 Tuesday treat time - order something that makes you happy! 🍽️💖"

**And unique messages for Wednesday, Thursday, and Friday!**

## 🔧 Troubleshooting

### Bot Not Sending Messages
- Check GitHub Actions logs for errors
- Verify both secrets are correctly set (TELEGRAM_BOT_TOKEN and TELEGRAM_CHAT_ID)
- Ensure the bot token is valid
- Confirm the chat ID is correct

### Wrong Timezone
- The bot is configured for Belarus/Minsk timezone (UTC+3)
- To change timezone, modify the timezone ID in `MessageService.cs` and `Program.cs`

## 🎯 Customization

### Change Schedule
Edit `.github/workflows/lunch-reminder.yml` cron expression:
```yaml
# Current: 8:00 AM Minsk (5:00 UTC) on weekdays
- cron: '0 5 * * 1-5'

# For 9:00 AM Minsk (6:00 UTC):
- cron: '0 6 * * 1-5'
```

### Customize Messages
Edit the message arrays in `MessageService.cs` to add your own personalized messages for each day.

### Change Time Window
Modify the time range in `Program.cs`:
```csharp
var reminderTime = new TimeOnly(8, 0);  // Start time
var endTime = new TimeOnly(8, 30);      // End time
```

## 💖 Contributing

This is a personal project, but feel free to fork it and customize it for your own loved ones!

## 📜 License

This project is open source and available under the MIT License.

---

Made with 💕 for the most important person in the world!
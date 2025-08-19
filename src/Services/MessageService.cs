namespace LunchReminderBot.Services;

public static class MessageService
{
    private static readonly string[] MondayMessages = [
        "Good morning my love! 💕 It's Monday - let's start the week with a delicious lunch! 🥗✨",
        "Hey beautiful! 😘 Monday lunch time - treat yourself to something amazing! 🍽️💖",
        "Morning sunshine! ☀️ New week, new lunch adventures! Don't forget to order something tasty! 🥙💕",
        "Hello gorgeous! 💖 Monday motivation: order the lunch that makes you smile! 😊🍜",
        "Good morning sweetheart! 🌸 Let's kick off Monday right with a wonderful lunch! 🥘💕"
    ];

    private static readonly string[] TuesdayMessages = [
        "Good morning beautiful! 💕 Tuesday vibes - time for a delightful lunch! 🥗✨",
        "Hey my love! 😘 Tuesday treat time - order something that makes you happy! 🍽️💖",
        "Morning darling! ☀️ Tuesday lunch planning - what sounds delicious today? 🥙💕",
        "Hello sweetie! 💖 Tuesday fuel up - don't forget your amazing lunch! 😊🍜",
        "Good morning angel! 🌸 Tuesday and time for lunch - treat yourself well! 🥘💕"
    ];

    private static readonly string[] WednesdayMessages = [
        "Good morning my heart! 💕 Wednesday - halfway to weekend, time for lunch! 🥗✨",
        "Hey gorgeous! 😘 Wednesday wisdom: good lunch = good day! 🍽️💖",
        "Morning beautiful! ☀️ Wednesday lunch break - what will make you smile? 🥙💕",
        "Hello my love! 💖 Wednesday energy boost - order something wonderful! 😊🍜",
        "Good morning sunshine! 🌸 Wednesday lunch time - you deserve something special! 🥘💕"
    ];

    private static readonly string[] ThursdayMessages = [
        "Good morning sweetheart! 💕 Thursday lunch - almost weekend, celebrate with food! 🥗✨",
        "Hey beautiful! 😘 Thursday treat - what delicious lunch will you choose? 🍽️💖",
        "Morning my love! ☀️ Thursday vibes - time for a fantastic lunch! 🥙💕",
        "Hello darling! 💖 Thursday lunch time - fuel up for the day ahead! 😊🍜",
        "Good morning angel! 🌸 Thursday and time to treat yourself to lunch! 🥘💕"
    ];

    private static readonly string[] FridayMessages = [
        "Good morning my love! 💕 Friday feeling - celebrate with an amazing lunch! 🥗✨",
        "Hey beautiful! 😘 TGIF lunch time - treat yourself to something special! 🍽️💖",
        "Morning gorgeous! ☀️ Friday vibes - what delicious lunch will end your week perfectly? 🥙💕",
        "Hello sweetheart! 💖 Friday fuel - order something that makes you happy! 😊🍜",
        "Good morning sunshine! 🌸 Friday lunch celebration - you've earned something wonderful! 🥘💕"
    ];

    private static readonly string[] GeneralMessages = [
        "Good morning my love! 💕 Don't forget to order your lunch today, I want to make sure you eat well! 🥗✨",
        "Hey beautiful! 😘 Time for lunch planning - treat yourself to something delicious! 🍽️💖",
        "Morning sunshine! ☀️ Lunch reminder from someone who loves you very much! 🥙💕",
        "Hello gorgeous! 💖 Time to fuel your amazing self with a wonderful lunch! 😊🍜",
        "Good morning sweetheart! 🌸 Lunch time - because you deserve all the best things! 🥘💕",
        "Hey my love! 💕 Quick reminder to take care of yourself with a good lunch today! 🥗✨",
        "Morning beautiful! 😘 Lunch o'clock - treat yourself like the queen you are! 🍽️💖",
        "Hello darling! ☀️ Don't forget lunch - your happiness matters to me! 🥙💕",
        "Good morning angel! 💖 Time for lunch - choose something that makes you smile! 😊🍜",
        "Hey sunshine! 🌸 Lunch reminder with extra love and hugs! 🥘💕"
    ];

    public static string GenerateReminder()
    {
        var random = new Random();
        return DateTime.Now.DayOfWeek switch
        {
            DayOfWeek.Monday => MondayMessages[random.Next(MondayMessages.Length)],
            DayOfWeek.Tuesday => TuesdayMessages[random.Next(TuesdayMessages.Length)],
            DayOfWeek.Wednesday => WednesdayMessages[random.Next(WednesdayMessages.Length)],
            DayOfWeek.Thursday => ThursdayMessages[random.Next(ThursdayMessages.Length)],
            DayOfWeek.Friday => FridayMessages[random.Next(FridayMessages.Length)],
            _ => GeneralMessages[random.Next(GeneralMessages.Length)]
        };
    }
}
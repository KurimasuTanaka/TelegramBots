using System;
using StudyBot.Services.Abstraction;

namespace StudyBot.Services.Notifications;

public class NotificationsMenu : State
{
    public NotificationsMenu(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
    }
}

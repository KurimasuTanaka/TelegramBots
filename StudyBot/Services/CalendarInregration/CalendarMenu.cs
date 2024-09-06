using System;
using StudyBot.Services.Abstraction;

namespace StudyBot.Services.CalendarInregration;

public class CalendarMenu : State
{
    public CalendarMenu(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
    }

}

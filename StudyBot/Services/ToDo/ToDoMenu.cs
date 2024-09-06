using System;
using StudyBot.Services.Abstraction;

namespace StudyBot.Services.ToDo;

public class ToDoMenu : State
{
    public ToDoMenu(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
    }
}

using System;
using StudyBot.Services.Abstraction;
using StudyBot.Services.CalendarInregration;
using StudyBot.Services.Notes;
using StudyBot.Services.Notifications;
using StudyBot.Services.ToDo;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace StudyBot.Services;

public class MainMenu : State
{
    public MainMenu(TelegramBotContext telegramBotContext) : 
        base(telegramBotContext)
    {
        textMessage = "Main menu";
        keyboardMarkup = new ReplyKeyboardMarkup(true).
        AddButtons("Notes", "ToDo").
        AddNewRow("Calendar", "Notifications");
    }

    public override void HandleAnswer(string answer)
    {
        if (_telegramBotContext == null)
        {
            throw new ArgumentNullException(nameof(_telegramBotContext));
        }

        switch (answer)
        {
            case "Notes":
                _telegramBotContext.state = new NotesMenu(_telegramBotContext);
                break;
            case "ToDo":
                _telegramBotContext.state = new ToDoMenu(_telegramBotContext);
                break;
            case "Calendar":
                _telegramBotContext.state = new CalendarMenu(_telegramBotContext);
                break;
            case "Notifications":
                _telegramBotContext.state = new NotificationsMenu(_telegramBotContext);
                break;
            default:
                _telegramBotContext.state = this;
                break;
        }
    }
}

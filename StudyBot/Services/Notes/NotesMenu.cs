using System;
using StudyBot.Services.Abstraction;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace StudyBot.Services.Notes;

public class NotesMenu : State
{
    public NotesMenu(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
        textMessage = "Notes menu";
        _keyboardMarkup = new ReplyKeyboardMarkup(true)
            .AddButton("Add note")
            .AddButton("Show notes")
            .AddNewRow()
            .AddButton("Back");
    }


    public override void HandleAnswer(string answer)
    {
        if(_telegramBotContext == null)
        {
            throw new ArgumentNullException(nameof(_telegramBotContext));
        }

        switch(answer)
        {
            case "Add note":
                _telegramBotContext.state = new NoteAddingState(_telegramBotContext);
                break;
            case "Show notes":
                _telegramBotContext.state = new NoteListState(_telegramBotContext);
                break;
            case "Back":
                _telegramBotContext.state = new MainMenu(_telegramBotContext);
                break;
            default:
                _telegramBotContext.state = this;
                break;
        }

    }    
}

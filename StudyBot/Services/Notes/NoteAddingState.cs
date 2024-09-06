using System;
using System.Net.Http.Json;
using SketchpadDatabase.Models;
using StudyBot.Services.Abstraction;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace StudyBot.Services.Notes;

public class NoteAddingState : State
{
    public NoteAddingState(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
        textMessage = "Write your note...";
        _keyboardMarkup = new ReplyKeyboardMarkup(true)
            .AddButton("Back");
    }

    private async void SentNote(string noteText)
    {
        HttpResponseMessage response = await _telegramBotContext.httpClient.PutAsJsonAsync<NoteModel>
            ($"http://localhost:5265/addNote", new NoteModel
            {
                Content = noteText,
                CreatedAt = DateTime.Now,
                chatId = _telegramBotContext.chatId
            });

        if (!response.IsSuccessStatusCode)
        {
            await _telegramBotContext.botClient.SendTextMessageAsync(
                chatId: _telegramBotContext.chatId,
                text: "Error while saving note"
            );
        }
        else
        {
            await _telegramBotContext.botClient.SendTextMessageAsync(
                chatId: _telegramBotContext.chatId,
                text: "Note is saved..."
        );

        }

    }

    public override void HandleAnswer(string answer)
    {

        SentNote(answer);

        if (_telegramBotContext == null)
        {
            throw new ArgumentNullException(nameof(_telegramBotContext));
        }

        _telegramBotContext.state = new NotesMenu(_telegramBotContext);

    }
}

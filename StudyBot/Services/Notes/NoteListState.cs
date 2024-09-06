using System;
using StudyBot.Services.Abstraction;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using SketchpadDatabase.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace StudyBot.Services.Notes;


public class NoteListState : State
{
    List<NoteModel> notes;

    public NoteListState(TelegramBotContext telegramBotContext) : base(telegramBotContext)
    {
        textMessage = "Notes list";
        _keyboardMarkup = new ReplyKeyboardMarkup(true)
            .AddButton("Back");

        ShowNotesList();
    }

    private async void ShowNotesList()
    {
        notes = new List<NoteModel>();

        HttpResponseMessage response = await _telegramBotContext.httpClient.GetAsync($"http://localhost:5265/getNotes/{_telegramBotContext.chatId}");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            notes = new List<NoteModel>(JsonConvert.DeserializeObject<NoteModel[]>(responseBody));
        }

        if (notes.Count == 0)
        {
            await _telegramBotContext.botClient.SendTextMessageAsync(
                chatId: _telegramBotContext.chatId,
                text: "No notes found"
            );
        }
        else
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
                notes.Select(note => new[]
                {
                    InlineKeyboardButton.WithCallbackData(note.Content, note.Id.ToString())
                })
            );

            await _telegramBotContext.botClient.SendTextMessageAsync(
                chatId: _telegramBotContext.chatId,
                text: "Choose note to view",
                replyMarkup: inlineKeyboard
            );
        }

    }

    public override void HandleAnswer(string answer)
    {

        switch (answer)
        {
            case "Back":
                _telegramBotContext.state = new NotesMenu(_telegramBotContext);
                break;
            default:
                _telegramBotContext.state = this;
                break;
        }

    }

    public override async void HandleCallbackQuery(CallbackQuery callbackQuery)
    {
        string reply = $"{notes.First(note => note.Id == int.Parse(callbackQuery.Data)).Content}\n<i>{notes.First(note => note.Id == int.Parse(callbackQuery.Data)).CreatedAt.ToString()}</i>";

        await _telegramBotContext.botClient.SendTextMessageAsync(
            chatId: _telegramBotContext.chatId,
            text: reply,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
        );

        await _telegramBotContext.botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
    }
}

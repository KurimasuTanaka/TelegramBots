using System;
using StudyBot.Services.Abstraction;
using Telegram.Bot;

namespace StudyBot.Services;

public class TelegramBotContext
{
    public State state;
    
    public ITelegramBotClient? botClient = null;
    public long chatId = 0;

    public HttpClient? httpClient = new HttpClient();

    public TelegramBotContext(ITelegramBotClient botClient, long chatId)
    {
        this.botClient = botClient;
        this.chatId = chatId;
        state = new MainMenu(this);
    }

}

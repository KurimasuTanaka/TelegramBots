using Microsoft.Extensions.Logging;
using StudyBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class UpdateHandler(ILogger<UpdateHandler> logger) : IUpdateHandler
{
    Dictionary<long, TelegramBotContext> chatStates = new Dictionary<long, TelegramBotContext>();

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        logger.LogError("Handle error: {error}", exception); // just dump the exception to the console

        return Task.CompletedTask;
    }

    public async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        long chatId = callbackQuery.From.Id;

        logger.LogInformation($"Query request recieved from user {chatId}");

        chatStates[chatId].state.HandleCallbackQuery(callbackQuery);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {


        if (update.Type is not UpdateType.Message) 
        {
            if(update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery!, cancellationToken);
            }
            return;
        }

        long chatId = update.Message!.Chat.Id;

        if (!chatStates.ContainsKey(chatId))
        {
            chatStates[chatId] = new TelegramBotContext(botClient, chatId);
        }

        logger.LogInformation($"Message recieved from user {chatId}: {update.Message?.Text}");


        chatStates[chatId].state.HandleAnswer(update.Message?.Text);


        Message recievedMessage = await botClient.SendTextMessageAsync(chatId, 
                                    chatStates[chatId].state.textMessage, 
                                    replyMarkup: chatStates[chatId].state.keyboardMarkup);


        logger.LogInformation($"State type is: {chatStates[chatId].state.GetType().Name}");
        
    }
}
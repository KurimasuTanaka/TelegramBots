using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class UpdateHandler(ILogger<UpdateHandler> logger) : IUpdateHandler
{
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        logger.LogError("Handle error: {error}", exception); // just dump the exception to the console

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Message recieved");
        Console.WriteLine("Message");

        if (update.Type is not UpdateType.Message) return;

        const string betrayal = "Зрада!!!";
        const string victory = "Перемога!!!";
        const string waiting = "Чекаємо далі...";

        var replyMarkup = new ReplyKeyboardMarkup(true).AddButtons(victory, betrayal);

        Message sent = await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Що сьогодні у нас?", replyMarkup: replyMarkup);

        Console.WriteLine(update.Message.Text);

        switch (update.Message.Text)
        {
            case victory:
                {
                    Console.WriteLine("Отправить победу");

                    await botClient.SendStickerAsync(update.Message.Chat.Id, "CAACAgIAAxkBAAEMpwRmvlpFplZPlGx91wPA75FBCnEJ4AAC8z4AAxgQSYWnfIdgfhyGNQQ");
                    break;
                }
            case betrayal:
                {
                    Console.WriteLine("Отправить зраду");
                    await botClient.SendStickerAsync(update.Message.Chat.Id, "CAACAgIAAxkBAAEMpwZmvlpKsj02CmMzRfQEZAjAibbXVQACp0IAAl73GUnOQFLPmGl_KzUE");
                    break;
                }
            default:
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, waiting);
                    break;
                }
        }
    }
}
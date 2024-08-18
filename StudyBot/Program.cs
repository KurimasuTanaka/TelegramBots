using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


using var cts = new CancellationTokenSource();

var bot = new TelegramBotClient("6933091380:AAFgKCmLK8_4thpwngId4PsIsEao9BqXaUc", cancellationToken: cts.Token);
var me = await bot.GetMeAsync();

const string betrayal = "Зрада!!!";
const string victory = "Перемога!!!";
const string waiting = "Чекаємо далі...";

var replyMarkup = new ReplyKeyboardMarkup(true).AddButtons(victory, betrayal);

bot.OnMessage += OnMessage;
bot.OnError += OnError;

Console.WriteLine("STARTING THE BOT");
Console.ReadLine();
cts.Cancel();


async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception); // just dump the exception to the console
}

async Task OnMessage(Message msg, UpdateType type)
{
    Message sent = await bot.SendTextMessageAsync(msg.Chat.Id, "Що сьогодні у нас?", replyMarkup: replyMarkup);

    Console.WriteLine(msg.Text);

    switch(msg.Text)
    {
        case victory:
        {
            Console.WriteLine("Отправить победу");

            await bot.SendStickerAsync(msg.Chat.Id, "CAACAgIAAxkBAAEMpwRmvlpFplZPlGx91wPA75FBCnEJ4AAC8z4AAxgQSYWnfIdgfhyGNQQ");
            break;
        }
        case betrayal:
        {
            Console.WriteLine("Отправить зраду");
            await bot.SendStickerAsync(msg.Chat.Id, "CAACAgIAAxkBAAEMpwZmvlpKsj02CmMzRfQEZAjAibbXVQACp0IAAl73GUnOQFLPmGl_KzUE");
            break;
        }
        default: 
        {
            await bot.SendTextMessageAsync(msg.Chat.Id, waiting);
            break;
        }
    }


}
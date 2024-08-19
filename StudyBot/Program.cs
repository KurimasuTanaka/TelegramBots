using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


IHost builder = Host.CreateDefaultBuilder(args).ConfigureServices(async (context, services) =>
{
    services.Configure<BotConfiguration>(context.Configuration.GetSection("BotConfiguration"));

    services.AddHttpClient("telegram_bot_client").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        BotConfiguration? botConfiguration = sp.GetService<IOptions<BotConfiguration>>()?.Value;
        TelegramBotClientOptions options = new(botConfiguration.BotToken);
        return new TelegramBotClient(options, httpClient);
    });

    services.AddScoped<UpdateHandler>();
    services.AddScoped<ReceiverService>();
    services.AddHostedService<PollingService>();
}).Build();


await builder.RunAsync();

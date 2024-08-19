
using Microsoft.Extensions.Logging;
using StudyBot.Abstract;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace StudyBot.Abstract;

public class ReceiverServiceBase<TUpdateHandler> : IReceiverService where TUpdateHandler : IUpdateHandler
{

    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IUpdateHandler _updateHandler;
    private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;

    public ReceiverServiceBase(ITelegramBotClient telegramBotClient, 
                                IUpdateHandler updateHandler, 
                                ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
    {
        _telegramBotClient = telegramBotClient;
        _updateHandler = updateHandler;
        _logger = logger;
    }

    public async Task ReceiveAsync(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = [],
            DropPendingUpdates = true,
        };

        var me = await _telegramBotClient.GetMeAsync(cancellationToken);
        _logger.LogInformation("Start receiving updates for {BotName}", me.Username);

        await _telegramBotClient.ReceiveAsync(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken);
    }
}
using Microsoft.Extensions.Logging;
using StudyBot.Abstract;
using Telegram.Bot;

public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler>> logger)
: ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);
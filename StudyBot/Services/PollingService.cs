
using Microsoft.Extensions.Logging;
using StudyBot.Abstract;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger) 
: PollingServiceBase<ReceiverService>(serviceProvider, logger);
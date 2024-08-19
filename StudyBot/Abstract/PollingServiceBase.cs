using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StudyBot.Abstract;

public abstract class PollingServiceBase<TReceiverService> : BackgroundService where TReceiverService : IReceiverService
{
    private IServiceProvider _serviceProvider;
    private ILogger _logger;

    public PollingServiceBase(IServiceProvider serviceProvider, ILogger<PollingServiceBase<TReceiverService>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting polling service");
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

                await receiver.ReceiveAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Polling failed with exception: {e}", e);

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }


        }
    }
}
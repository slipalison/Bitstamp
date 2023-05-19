using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infra.MassTransitConfiguration.Consumers;

public class ToDoConsumer : IConsumer<ToDoItemQueueCreateCommand>
{
    private readonly IBitstampService _BitstampService;
    private readonly ILogger<ToDoConsumer> _logger;

    public ToDoConsumer(ILoggerFactory factory, IBitstampService BitstampService)
    {
        _BitstampService = BitstampService;
        _logger = factory.CreateLogger<ToDoConsumer>();
    }

    public async Task Consume(ConsumeContext<ToDoItemQueueCreateCommand> context)
    {
        _logger.LogInformation("Mensagem recebida {@Message}", context.Message);
        var entity = await _BitstampService.Create(new ToDoItemCreateCommand
        {
            Deadline = context.Message.Deadline,
            Name = context.Message.Name
        });
        _logger.LogInformation("Tarefa cadastrada {@Message}", entity);

        await context.NotifyConsumed(context, TimeSpan.Zero, nameof(ToDoConsumer));
        
        _logger.LogInformation("Mensagem concluida {@Message}", context.Message);
    }
}